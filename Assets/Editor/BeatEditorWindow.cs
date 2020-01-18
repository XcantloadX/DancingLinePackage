using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TriggerEditorWindow : EditorWindow 
{
    TriggerEditorWindow()
    {
        this.titleContent.text = "Trigger Editor";
    }

    //打开本窗口
    [MenuItem("Beat Trigger/ Editor")]
    private static void OpenWindow()
    {
        EditorWindow.GetWindow(typeof(TriggerEditorWindow));
    }

    //绘制GUI
    private void OnGUI()
    {
        EditorGUILayout.LabelField("File:");


        EditorGUILayout.LabelField("Time:");
        EditorGUILayout.Slider(0, 0, 100);
    }
}
