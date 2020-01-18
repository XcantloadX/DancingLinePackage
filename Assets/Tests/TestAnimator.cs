using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Music;

public class TestAnimator : BeatTrigger 
{
    public Vector3 to;
    public float time;
    public bool started = false;


	void Update () 
    {
        if(this.started)
            this.transform.position += new Vector3(0, 2, 0) * Time.deltaTime;
	}

    protected override void OnBeatTrigger()
    {
        this.started = true;
    }
}
