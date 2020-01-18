using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Music;

/// <summary>
/// 绑定节奏点与动画
/// </summary>
public class BeatAnimationBinding : BeatTrigger 
{
    public Animation anim;

    protected void Start()
    {
        base.Start();
        this.anim = this.GetComponent<Animation>();
        if (this.anim == null)
            throw new Exception("Add a Animation first!");
    }

    protected override void OnBeatTrigger()
    {
        this.anim.Play();
    }
}
