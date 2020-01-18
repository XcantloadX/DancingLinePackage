using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace XcantloadX.DL.Music
{
    public abstract class BeatTrigger : MonoBehaviour 
    {
        //public float beatPosition = 0;
        public int beatIndex;
        public bool isEnabled = true;
        private MusicPlayer player;

        protected void Start () 
        {
            this.player = MusicPlayer.Main;
            this.player.BeatEvent += this.OnBeatEvent; //挂接事件
        }

        protected void OnBeatEvent(double time, double beat, int beatIndex)
        {
            if (this.isEnabled && (beatIndex == this.beatIndex))
            {
                this.OnBeatTrigger();
            }
        }

        protected abstract void OnBeatTrigger();
    }
}
