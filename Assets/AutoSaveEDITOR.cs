using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]

public class AutoSaveEDITOR:MonoBehaviour
{
    static AutoSaveEDITOR()
    {
        EditorApplication.playModeStateChanged += SaveOnPlay;
    }

    private static void SaveOnPlay(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingEditMode)
        {           
            EditorSceneManager.SaveOpenScenes();
            AssetDatabase.SaveAssets();
        }
    }
}
