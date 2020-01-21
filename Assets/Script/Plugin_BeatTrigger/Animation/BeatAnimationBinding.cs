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
    public new Animation animation;

    protected new void Start()
    {
        base.Start();
        this.animation = this.GetComponent<Animation>();
        if (this.animation == null)
            throw new Exception("Add a Animation first!");
    }

    protected override void OnBeatTrigger()
    {
        this.animation.Play();
    }
}
