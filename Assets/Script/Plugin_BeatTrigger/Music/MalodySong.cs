using System.Collections;
using System.Collections.Generic;
using MojoJson;

/// <summary>
/// 歌曲
/// </summary>
public class MalodySong
{
	/// <summary>
	/// 歌曲的BPM
	/// </summary>
	public float Bpm { get; private set; }

	/// <summary>
	/// 歌曲的长度（单位：秒）
	/// </summary>
	public float LengthSec{ get; private set; }

	/// <summary>
	/// 每一拍的长度（单位：秒）
	/// </summary>
	public float SecPerBeat{ get; private set; }

    /// <summary>
    /// 歌曲的偏移（延迟多久播放）（单位：秒）
    /// </summary>
    public float Offset {get; private set;}

	/// <summary>
	/// 所有的节奏点
	/// </summary>
	/// <value>The beats.</value>
	public float[] Beats { get; private set; }

	/// <summary>
	/// 从 Malody 谱面创建 Song 对象
	/// </summary>
	/// <param name="json">Json 文本</param>
	public static MalodySong CreateFromMalody(string jsonString)
	{
		//解析 json
		MojoJson.JsonValue jsonObj = MojoJson.Json.Parse(jsonString);
		MalodySong song = new MalodySong();

		//获取所有节拍
		List<MojoJson.JsonValue> jsonBeats = jsonObj.AsObjectGetArray("note");
		song.Beats = new float[jsonBeats.Count - 1]; //第一个节拍为播放音乐，没有实际作用

		for (int i = 0; i < jsonBeats.Count; i++)
		{
			//"beat": [0,0,4] 第一个为节拍数，第二个为几分之几节拍，前两个由 0 数起
			//例如：[0,0,4] 为 一拍 + 四分之一拍
			//		[1,2,4] 为 一拍 + 四分之三拍

			MojoJson.JsonValue jsonBeat = jsonBeats[i];
			int beatNum = jsonBeat.AsObjectGetArray("beat")[0].AsInt();
			int subBeatNum = jsonBeat.AsObjectGetArray("beat")[1].AsInt();
			int subBeatPerBeat = jsonBeat.AsObjectGetArray("beat")[2].AsInt();
            if (beatNum == 0 && subBeatNum == 0 && subBeatPerBeat == 1) //此拍为播放音频，不表示实际节奏点
            {
                //获取偏移
                //if (!jsonBeat.AsObjectGetIsNull("offset"))
                if (jsonString.Contains("offset"))
                    song.Offset = jsonBeat.AsObjectGetFloat("offset") / 1000; //毫秒转为秒
                else
                    song.Offset = 0;
                continue;
            }

			song.Beats[i] = (beatNum) + ((subBeatNum) / (float)subBeatPerBeat); //强制转换成浮点数运算，避免自动取整
		}

		//获取 bpm
		song.Bpm = jsonObj.AsObjectGetArray("time")[0].AsObjectGetFloat("bpm"); //TODO:加入分段bpm的支持
		song.SecPerBeat = 60f / song.Bpm;
		return song;
	}

    /// <summary>
    /// 将秒转换为节拍
    /// </summary>
    /// <param name="time">秒</param>
    /// <returns>节拍</returns>
    public double Time2Beat(double time)
    {
        return Bpm * time / 60f;
    }

    /// <summary>
    /// 将节拍转换为秒
    /// </summary>
    /// <param name="beat">节拍</param>
    /// <returns>秒</returns>
    public double Beat2Time(double beat)
    {
        return beat / Bpm * 60f;
    }
}
