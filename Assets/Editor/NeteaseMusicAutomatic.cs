using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEditor;

#if UNITY_5
#if UNITY_EDITOR_WIN

// 来回切网易云音乐实在是太麻烦了
[InitializeOnLoadAttribute]
public static class NeteaseMusicAutomatic
{
    private static bool isMusicAppRunning = false;

    [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
    public static extern void keybd_event(int bVk, byte bScan, uint dwFlags, uint dwExtraInfo);

    public const int KEYEVENTF_KEYUP = 2;

    static NeteaseMusicAutomatic()
    {
        EditorApplication.playmodeStateChanged += PlayModeState;

        //判断网易云音乐是否运行
        System.Diagnostics.Process[] processList = System.Diagnostics.Process.GetProcesses();
        foreach (System.Diagnostics.Process process in processList)
        {
            if (process.ProcessName.ToLower() == "cloudmusic")
                isMusicAppRunning = true;
        }
    }

    private static void PlayModeState()
    {
        if (isMusicAppRunning && (EditorState.willEnterPlayMode || EditorState.willExitPlayMode))
        {
            keybd_event(17, 0, 0, 0); //Ctrl = 17
            keybd_event(18, 0, 0, 0); //Alt = 18
            keybd_event(80, 0, 0, 0); //P = 80
            keybd_event(17, 0, KEYEVENTF_KEYUP, 0);
            keybd_event(18, 0, KEYEVENTF_KEYUP, 0);
        }
    }
}

/// <summary>
/// 获取 Editor 的运行状态，Unity 5 以上不需要
/// </summary>
public static class EditorState {

    public static bool willEnterPlayMode {
        get { return !EditorApplication.isPlaying
                     && EditorApplication.isPlayingOrWillChangePlaymode; }
    }

    public static bool didEnterPlayMode {
        get { return EditorApplication.isPlaying
                     && EditorApplication.isPlayingOrWillChangePlaymode; }
    }

    public static bool willExitPlayMode {
        get { return EditorApplication.isPlaying
                     && !EditorApplication.isPlayingOrWillChangePlaymode; }
    }

    public static bool didExitPlayMode {
        get { return !EditorApplication.isPlaying
                     && !EditorApplication.isPlayingOrWillChangePlaymode; }
    }
}

#endif
#endif

