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

	void Start () 
    {
        NullCheck.Run(this);
        musicPlayer.BeatEvent += OnBeatEvent;
	}
	
	void Update () 
    {
	}

    private void OnBeatEvent(double timePosition, double beatPosition, int beatIndex)
    {
        line.Rotate();
    }

    //绘制 Auto Mode 文本
    private void OnGUI()
    {
        GUIStyle style = new GUIStyle();
        style.fontSize = 25;
        style.normal.textColor = Color.white;

        GUI.Label(new Rect(0, 0, 300, 200), "AutoPlay", style);
    }
}
