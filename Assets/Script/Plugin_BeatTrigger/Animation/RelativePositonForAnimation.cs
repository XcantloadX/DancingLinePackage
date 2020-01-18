using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 此脚本使得在 Animation 中可以使用相对坐标
/// </summary>
[RequireComponent(typeof(Animation))]
public class RelativePositonForAnimation : MonoBehaviour 
{
    [HideInInspector] public Vector3 positon;
    private Vector3 startPosition;

	void Start () 
    {
        this.startPosition = this.transform.position;
	}
	
	
	void Update () 
    {
        Vector3 newPos = this.startPosition + this.positon;
        if(newPos != this.startPosition) //没有在动画中使用此脚本的情况
            this.transform.position = newPos;
	}
}
