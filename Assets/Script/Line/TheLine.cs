using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XcantloadX.DL.Main
{
    public class TheLine : MonoBehaviour 
    {
        private bool isStarted = false;
        public float speed = 8f;
        private int direction = 1;

        //线的身体
        private GameObject body;
        private GameObject bodyParent;
        private BoxCollider bodyCollider;

        [NullCheck]
        public AudioClip hitSound;

        public Color lineColor = Color.white;

        public delegate void OnRotate(int direction);
        public event OnRotate RotateEvent;

        public const string OBSTACLE = "Obstacle";

        void Start () 
        {
            NullCheck.Run(this);
            this.GetComponent<MeshRenderer>().material.color = this.lineColor;
        }


        void Update () 
        {
            if (!this.isStarted)
                return;

            Move();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == TheLine.OBSTACLE)
                KillLine();
        }

        /// <summary>
        /// 开始移动线
        /// </summary>
        public void StartLine()
        {
            this.bodyParent = new GameObject("LineBodies");
            CreateBody();
            this.isStarted = true;
        }

        /// <summary>
        /// 杀死线
        /// </summary>
        public void KillLine()
        {
            LevelContoller.instance.StopLevel();
            this.isStarted = false;
            AudioSource.PlayClipAtPoint(this.hitSound, Camera.main.transform.position);
        }

        /// <summary>
        /// 转向
        /// </summary>
        public void Rotate()
        {
            this.direction = (this.direction == 1) ? 0 : 1;
            this.transform.rotation = Quaternion.Euler(0, 90 * this.direction, 0);

            CreateBody();

            if (this.RotateEvent != null)
                this.RotateEvent(this.direction);
        }

        //移动
        private void Move()
        {
            Vector3 forward = this.transform.forward;
            //这两句很重要！
            //由于某些奇怪的原因，x 或 z 有时为很小很小的数字（接近于 0），导致 x == 0 返回 false，下面的判断会出 bug
            forward.x = (float)Math.Round(forward.x, 0);
            forward.z = (float)Math.Round(forward.z, 0);

            this.transform.position += forward * this.speed * Time.deltaTime; //线头的移动

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

        //创建线的身体部分
        private void CreateBody()
        {
            this.body = GameObject.CreatePrimitive(PrimitiveType.Cube);

            //
            this.body.transform.parent = this.bodyParent.transform;

            //设置颜色
            this.body.GetComponent<MeshRenderer>().material.color = this.lineColor;

            //取消碰撞
            this.bodyCollider = this.body.GetComponent<BoxCollider>(); 
            this.bodyCollider.isTrigger = true;

            Vector3 forward = this.transform.forward;
            //这两句很重要！
            //由于某些奇怪的原因，x 或 z 有时为很小很小的数字（接近于 0），导致 x == 0 返回 false，下面的判断会出 bug
            forward.x = (float)Math.Round(forward.x, 0);
            forward.z = (float)Math.Round(forward.z, 0);

            if (forward.z != 0)
                this.body.transform.localScale = new Vector3(1, 1, 0);
            else if (forward.x != 0)
                this.body.transform.localScale = new Vector3(0, 1, 1);
        }
    }
}

