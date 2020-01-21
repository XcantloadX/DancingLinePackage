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

        void Start () 
        {

        }


        void Update () 
        {
            if (!this.isStarted)
                return;

            Move();
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
        /// 转向
        /// </summary>
        public void Rotate()
        {
            this.direction = (this.direction == 1) ? 0 : 1;
            this.transform.rotation = Quaternion.Euler(0, 90 * this.direction, 0);

            CreateBody();
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

        private void CreateBody()
        {
            this.body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.body.transform.parent = this.bodyParent.transform;
            this.bodyCollider = this.body.GetComponent<BoxCollider>(); //取消碰撞
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

