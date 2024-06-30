using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using RenderHeads.Media.AVProVideo;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(AVProVideoManager))]
public class AVProVideoManagerEditor : Editor
{
    private AVProVideoManager mAVProVideoManager;
    private IMediaControl mControl;

    private void OnEnable()
    {
        mAVProVideoManager = (AVProVideoManager) target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);

        GUILayout.Label("By Player Prefs");
        GUILayout.Space(10);

        mAVProVideoManager.byPlayerPrefs = EditorGUILayout.Toggle(mAVProVideoManager.byPlayerPrefs, GUILayout.Width(16));

        GUILayout.Space(10);
        GUILayout.EndHorizontal();


        GUILayout.BeginHorizontal();
        GUILayout.Space(10);

        GUILayout.Label("Video Name");
        GUILayout.Space(10);

        if (mAVProVideoManager.byPlayerPrefs)
        {
            mAVProVideoManager.videoName = PlayerPrefs.GetString(PlayerPrefsKeys.Video360Name, "Not Set");

            GUI.enabled = false;
            EditorGUILayout.TextField(mAVProVideoManager.videoName);
            GUI.enabled = true;
        }
        else
        {
            mAVProVideoManager.videoName = EditorGUILayout.TextField(mAVProVideoManager.videoName);
        }

        GUILayout.Space(10);
        GUILayout.EndHorizontal();
        
        GUILayout.Space(10);
        GUILayout.EndVertical();

        if (Application.isPlaying)
        {
            mControl = mAVProVideoManager.GetControl();

            if(mControl.CanPlay())
            {
                GUILayout.BeginVertical("box");
                GUILayout.Space(10);

                GUILayout.BeginHorizontal();
                GUILayout.Space(10);

                if (mControl.IsFinished() || !mControl.IsPlaying())
                {
                    if (GUILayout.Button("Play"))
                    {
                        mAVProVideoManager.Play();
                    }
                }

                if (!mControl.IsFinished() && mControl.IsPlaying())
                {
                    if (GUILayout.Button("Pause"))
                    {
                        mAVProVideoManager.Pause();
                    }

                    if (GUILayout.Button("Replay"))
                    {
                        mAVProVideoManager.Replay();
                    }
                }

                GUILayout.Space(10);
                GUILayout.EndHorizontal();

                GUILayout.Space(10);
                GUILayout.EndVertical();
            }
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(mAVProVideoManager);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
