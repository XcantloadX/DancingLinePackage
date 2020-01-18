using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Music;

namespace XcantloadX.DL.Main
{
    public class LevelContoller : MonoBehaviour 
    {
        public TheLine line;
        public MusicPlayer musicPlayer;
        public bool isStarted = false;


        // Use this for initialization
        void Start () 
        {

        }

        // Update is called once per frame
        void Update () 
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!this.isStarted)
                    this.StartLevel();

                this.line.Rotate();
            }
        }

        /// <summary>
        /// 开始关卡
        /// </summary>
        public void StartLevel()
        {
            this.isStarted = true;
            this.line.StartLine();
            this.musicPlayer.Play();
        }
    }
}

