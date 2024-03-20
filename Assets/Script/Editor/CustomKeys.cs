using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CustomKeys
{
    [MenuItem("Tools/ShortKey/暂停 _F5")]
    static void EditorPauseCommand()
    {
        EditorApplication.isPaused = !EditorApplication.isPaused;
    }
}
