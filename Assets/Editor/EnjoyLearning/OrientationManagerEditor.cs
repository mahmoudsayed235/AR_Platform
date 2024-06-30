using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using EnjoyLearning.VR.SDK;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


[CustomEditor(typeof(OrientationManager))]
public class OrientationManagerEditor : Editor
{
    private OrientationManager mOrientationManager;

    private void OnEnable()
    {
        mOrientationManager = (OrientationManager) target;
    }

    public override void OnInspectorGUI()
    {
        int tempInt;

        // begin box 1
        EditorGUILayout.BeginVertical("box");
        GUILayout.Space(5);

        // begin row 1
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);

        GUILayout.Label("Force Size", GUILayout.Width(128));
        GUILayout.Space(5);

        mOrientationManager.forceSize = EditorGUILayout.Toggle(mOrientationManager.forceSize, GUILayout.Width(16));

        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
        // end row 1

        if(mOrientationManager.forceSize)
        {
            // begin row 1
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(5);

            GUILayout.Label("Screen Width", GUILayout.Width(128));
            GUILayout.Space(5);
            int.TryParse(EditorGUILayout.TextField(mOrientationManager.screenSize.Width.ToString()), out tempInt);
            mOrientationManager.screenSize.Width = tempInt;

            GUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
            // end row 1


            // begin row 2
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(5);

            GUILayout.Label("Screen Height", GUILayout.Width(128));
            GUILayout.Space(5);
            int.TryParse(EditorGUILayout.TextField(mOrientationManager.screenSize.Height.ToString()), out tempInt);
            mOrientationManager.screenSize.Height = tempInt;

            GUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
            // end row 2
        }

        // begin row 3
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);

        GUILayout.Label("Screen Orientation", GUILayout.Width(128));
        GUILayout.Space(5);

        mOrientationManager.screenOrientation = (ScreenOrientation) EditorGUILayout.EnumPopup(mOrientationManager.screenOrientation);

        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
        // end row 3


        GUILayout.Space(5);
        EditorGUILayout.EndVertical();
        // end box 1

        if (GUI.changed)
        {
            EditorUtility.SetDirty(mOrientationManager);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
