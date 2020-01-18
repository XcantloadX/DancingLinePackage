using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XcantloadX.DL.Main
{
    public class TheLine : MonoBehaviour 
    {
        public bool isStarted = false;
        public float speed = 5f;
        private int direction = 1;
        private GameObject body;
        private Mesh bodyMesh;

        void Start () 
        {

        }


        void Update () 
        {
            if (!this.isStarted)
                return;

            Move();
        }

        public void StartLine()
        {
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
        }

        //移动
        private void Move()
        {
            this.transform.position += this.transform.forward * this.speed * Time.deltaTime;
            //this.body.transform.position = this.transform.position - new Vector3(0, 0, this.body.transform.localScale.z * this.bodyMesh.bounds.size.z / 2);
        }

        private void CreateBody()
        {
            this.body = GameObject.CreatePrimitive(PrimitiveType.Cube);
            this.bodyMesh = this.body.GetComponent<MeshFilter>().mesh;
        }
    }
}

