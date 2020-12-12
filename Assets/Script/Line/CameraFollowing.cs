using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XcantloadX.DL.Main
{
    /// <summary>
    /// 相机跟随
    /// </summary>
    [RequireComponent(typeof(Camera))]
    public class CameraFollowing : MonoBehaviour 
    {
        public Transform target;
        private Vector3 offset;

        void Start () 
        {
            this.offset = this.transform.position - this.target.position;
        }
            
        void Update () 
        {
            this.transform.position = Vector3.Lerp(this.transform.position, this.target.position + offset, Time.deltaTime * 8f);
        }
    }
}

