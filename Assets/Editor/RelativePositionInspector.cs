using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(RelativePositonForAnimation))]
public class RelativePositionInspector : Editor 
{
    private RelativePositonForAnimation instance;
    private bool isShowing = false;
    private Vector3 startPositon;

    private void OnEnable()
    {
        this.instance = (RelativePositonForAnimation)this.target;
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.BeginVertical();

        EditorGUILayout.LabelField("[Editor]Relative Position: ");
        ShowRelativePositon();
        if (GUILayout.Button(this.isShowing ? "Hide" : "Show"))
        {
            this.isShowing = !this.isShowing;
            if (this.isShowing)
                this.startPositon = instance.transform.position; //获取开始坐标
        }



        EditorGUILayout.EndVertical();
    }

    //显示相对坐标
    private void ShowRelativePositon()
    {
        if (this.isShowing)
        {
            EditorGUILayout.Vector3Field("Position", this.instance.transform.position - this.startPositon);
        }
    }
}
