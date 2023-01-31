using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

[CustomEditor(typeof(LevelEditor))]
public class LevelEditor_inspector : Editor
{
    public GameObject test;
    public override void OnInspectorGUI()
    {
        LevelEditor levelEditor = (LevelEditor)target;
        //levelEditor.nameId = EditorGUILayout.field("Name id", levelEditor.nameId);
    
        if (GUILayout.Button("Save level"))
        {
            levelEditor.SaveLevel();
            
        }
    }
    


}
