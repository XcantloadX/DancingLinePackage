using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XcantloadX.DL.Music;

namespace XcantloadX.DL.Main
{
    /// <summary>
    /// 关卡控制器
    /// </summary>
    public class LevelContoller : MonoBehaviour 
    {
        public TheLine line;
        public MusicPlayer musicPlayer;
        public bool isStarted = false;

        void Start () 
        {

        }
            
        void Update () 
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
            {
                if (!this.isStarted)
                {
                    StartLevel();
                    return;
                }
                    

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

