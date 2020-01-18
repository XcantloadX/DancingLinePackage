using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 音乐播放器
/// </summary>
namespace XcantloadX.DL.Music
{
    public class MusicPlayer : MonoBehaviour
    {

        private AudioSource audioSource;
        public MalodySong Song { get; private set;}
        [SerializeField] private AudioClip audioClip; //待播放的 AudioClip
        public string beatmapPath;

        [SerializeField] private double startTime = 0;
        [SerializeField] private double timePosition = 0; //当前的播放位置（秒）
        [SerializeField] private double beatPosition = 0; //当前的播放位置（节拍）
        [SerializeField] public int currentIndex = 0;

        private bool isInited = false; //是否已经初始化

        private static MusicPlayer _main = null;
        public static MusicPlayer Main
        {
            get
            {
                if (MusicPlayer._main == null)
                    MusicPlayer._main = GameObject.FindObjectOfType<MusicPlayer>();
                return MusicPlayer._main;
            }
        }

        //节奏点事件
        public delegate void BeatHandler(double timePosition, double beatPosition, int beatIndex);
        public event BeatHandler BeatEvent;

        //节奏点丢失事件
        public delegate void BeatLossHandler(double lostBeatPosition, double currentBeatPosition);
        public event BeatLossHandler BeatLossEvent;

        public bool IsPlaying{ get; private set; }

        /// <summary>
        /// 进行时间对比时允许的误差范围
        /// </summary>
        [Tooltip("The time offset when compare the current time with the beat time.")]
        public float AllowedTimeOffset = 0.15f;

        /// <summary>
        /// 是否忽略节奏点丢失
        /// </summary>
        public bool IsBeatLossIgnored = false;

        void Start()
        {
            //TODO:设计Editor GUI

            Init();
        }


        void Update()
        {
            if (!this.IsPlaying || !this.isInited)
                return;

            //计算当前播放位置
            timePosition = audioSource.time + this.Song.Offset;
            beatPosition = (float)Math.Round(this.timePosition / this.Song.SecPerBeat, 1);
            if (timePosition > audioSource.clip.length) //如果播放完毕
            { 
                this.IsPlaying = false;
                return;
            }
            if (this.currentIndex >= this.Song.Beats.Length)
                return;


            //比较节拍位置
            if (Math.Abs(beatPosition - Song.Beats[this.currentIndex]) <= this.AllowedTimeOffset)
            {
                if(this.BeatEvent != null)
                    this.BeatEvent(this.timePosition, this.beatPosition, this.currentIndex); //触发事件
                currentIndex++;
            }
            else if (beatPosition >= Song.Beats[currentIndex]) //如果当前位置比下一次节拍位置要大，说明上一次没有检测到
            {
                Debug.LogWarning("Detected beat loss!");

                if(this.IsBeatLossIgnored && this.BeatEvent != null)
                    this.BeatEvent(this.timePosition, this.beatPosition, this.currentIndex);
                else if(this.BeatLossEvent != null)
                    this.BeatLossEvent(Song.Beats[currentIndex], beatPosition); //触发事件

                while (beatPosition >= Song.Beats[currentIndex])
                    currentIndex++;
            }

        }

        //初始化脚本
        private void Init()
        {
            if (this.isInited)
                return;

            //检查 AudioClip
            if (this.audioClip == null)
                throw new Exception("Please attach a AudioClip!");

            //加载 AudioSource
            if (this.audioSource == null)
            {
                this.audioSource = Camera.main.gameObject.AddComponent<AudioSource>();
                this.audioSource.clip = this.audioClip;
            }

            //检查谱面文件
            if (string.IsNullOrEmpty(this.beatmapPath) || !System.IO.File.Exists(this.beatmapPath))
                throw new Exception("The beatmap path is empty or invalid.");
            else
                LoadSong(MalodySong.CreateFromMalody(System.IO.File.ReadAllText(this.beatmapPath)));

            this.isInited = true;
        }

        /// <summary>
        /// 加载指定歌曲
        /// </summary>
        /// <param name="song">Song.</param>
        public void LoadSong(MalodySong song)
        {
            this.Song = song;
        }

        /// <summary>
        /// 播放音频并开始触发 Trigger
        /// </summary>
        public void Play()
        {
            if (this.Song == null)
                throw new Exception("Please load a song first!");
            if (this.audioSource == null)
                Init();

            startTime = AudioSettings.dspTime;
            this.audioSource.Play();
            this.IsPlaying = true;
        }

        /// <summary>
        /// 暂停播放
        /// </summary>
        public void Pause()
        {
            this.audioSource.Pause();
            this.IsPlaying = false;
        }
    }

}
