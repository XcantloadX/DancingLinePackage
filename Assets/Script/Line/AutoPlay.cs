using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Main;
using XcantloadX.DL.Music;

/// <summary>
/// 自动模式
/// </summary>
public class AutoPlay : MonoBehaviour 
{
    [NullCheck]
    public TheLine line;
    [NullCheck]
    public MusicPlayer musicPlayer;

    private float timer = 0;
    private bool isShow = false;

	void Start () 
    {
        NullCheck.Check(this);
        musicPlayer.BeatEvent += OnBeatEvent;
	}
	
	void Update () 
    {
        //计时器
        //实现闪烁“Auto Mode”文本
        timer += Time.deltaTime;
        if (timer >= 0.5f)
        {
            isShow = !isShow;
            timer = 0;
        }
	}

    private void OnBeatEvent(double timePosition, double beatPosition, int beatIndex)
    {
        line.Rotate();
    }

    //绘制 Auto Mode 文本
    private void OnGUI()
    {
        if (!isShow)
            return;

        GUIStyle style = new GUIStyle();
        style.fontSize = 30;
        style.normal.textColor = Color.red;

        GUI.Label(new Rect(0, 0, 300, 200), "Auto Mode", style);
    }
}
