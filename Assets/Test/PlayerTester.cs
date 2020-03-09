using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Music;

public class PlayerTester : MonoBehaviour 
{

    public MusicPlayer controller;
    public AudioClip tickClip;
    public bool autoStart = false;

	// Use this for initialization
	void Start () 
    {
        //丑小鸭：D:\\Malody\\beatmap\\_song_9421\\0\\1543646563.mc
        //赛车：D:/Malody/beatmap/_song_9932/0/1546327363.mc
        //狂妄之人：D:\Malody\beatmap\_temp_1550817018\1550817018.mc

        controller.BeatEvent += OnBeatTrigger;
        if(autoStart)
        {
            controller.LoadSong(MalodySong.CreateFromMalody(System.IO.File.ReadAllText("D:/Malody/beatmap/_song_9932/0/1546327363.mc")));
            controller.Play();
        }
	}
	
    private void OnBeatTrigger(double Time, double beat, int beatIndex)
    {
        AudioSource.PlayClipAtPoint(tickClip, Camera.main.transform.position);
    }

	// Update is called once per frame
	void Update () 
    {
		
	}
}
