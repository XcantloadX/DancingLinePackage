using System;
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
        private bool isStarted = false;

        private static LevelContoller _ins = null;
        public static LevelContoller instance
        {
            get
            {
                if (_ins == null)
                    _ins = GameObject.FindObjectOfType<LevelContoller>();
                return _ins;
            }
        }


        void Start () 
        {
            if (this.musicPlayer == null)
                Debug.LogWarning("No MusicPlayer.");
            if (this.line == null)
                throw new Exception("No Line.");
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
        /// 开始游戏
        /// </summary>
        public void StartLevel()
        {
            this.isStarted = true;
            this.line.StartLine();
            if(this.musicPlayer != null)
                this.musicPlayer.Play();
        }

        /// <summary>
        /// 停止游戏
        /// </summary>
        public void StopLevel()
        {
            this.musicPlayer.Pause();
        }
    }
}

