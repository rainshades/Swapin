#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.Diagnostics; 

[CustomEditor(typeof(BuildAssist))]
public class BuildAssistEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        var BA = target as BuildAssist;
        if (GUILayout.Button("Build"))
        {
            BA.Build(); 
        }
    }
}

public class BuildAssist : MonoBehaviour
{
    [SerializeField] int CurrentVersion, BundleVersion; 
    public void Build()
    {
        BundleVersion++; 
        PlayerSettings.bundleVersion = $"{BundleVersion}";
        var Build = BuildPlayerWindow.DefaultBuildMethods.GetBuildPlayerOptions(new BuildPlayerOptions()); 
        BuildPipeline.BuildPlayer(Build); 
    }
}
#endif