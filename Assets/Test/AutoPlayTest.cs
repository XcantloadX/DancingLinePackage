using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Main;
using XcantloadX.DL.Music;

public class AutoPlayTest : MonoBehaviour 
{
    public TheLine line;
    public MusicPlayer musicPlayer;

	void Start () 
    {
        musicPlayer.BeatEvent += OnBeatEvent;
	}
	
	void Update () 
    {
		
	}

    private void OnBeatEvent(double timePosition, double beatPosition, int beatIndex)
    {
        line.Rotate();
    }
}
