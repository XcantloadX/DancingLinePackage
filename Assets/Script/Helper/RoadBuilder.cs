/*using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Main;


public class RoadBuilder : MonoBehaviour 
{
    public TheLine line;
    public bool isEnabled = true;

    private GameObject wall;
    private GameObject wallParent;
    private BoxCollider wallCollider;

	void Start () 
    {
        line.RotateEvent += LineRotateEvent;
        this.wallParent = new GameObject("Walls");
	}
	
	void Update () 
    {
        //线身体的伸长
        if (forward.z != 0)
            this.body.transform.localScale += new Vector3(0, 0, this.speed * Time.deltaTime);
        else if (forward.x != 0)
            this.body.transform.localScale += new Vector3(this.speed * Time.deltaTime, 0, 0);

        //线身体的移动
        if (forward.z != 0)
            this.body.transform.position = this.transform.position - new Vector3(0, 0, forward.z * (this.bodyCollider.bounds.size.z / 2 + 0.5f)); //0.5f 为线头 scale.z / 2
        else if (forward.x != 0)
            this.body.transform.position = this.transform.position - new Vector3(forward.x * (this.bodyCollider.bounds.size.x / 2 + 0.5f), 0, 0);
	}

    private void LineRotateEvent(int d)
    {
        CreateBody();
    }

    private void CreateBody()
    {
        this.wall = GameObject.CreatePrimitive(PrimitiveType.Cube);
        this.wall.transform.parent = this.wallParent.transform;
        this.wallCollider = this.wall.GetComponent<BoxCollider>(); //取消碰撞

        Vector3 forward = this.transform.forward;
        //这两句很重要！
        //由于某些奇怪的原因，x 或 z 有时为很小很小的数字（接近于 0），导致 x == 0 返回 false，下面的判断会出 bug
        forward.x = (float)Math.Round(forward.x, 0);
        forward.z = (float)Math.Round(forward.z, 0);

        if (forward.z != 0)
            this.wall.transform.localScale = new Vector3(1, 1, 0);
        else if (forward.x != 0)
            this.wall.transform.localScale = new Vector3(0, 1, 1);
    }
}
*/