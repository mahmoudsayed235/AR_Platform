using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Video;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(VideoPlayer))]
public class VideoPlayerEditor : Editor
{
    private VideoPlayer videoPlayer;

    private void OnEnable()
    {
        videoPlayer = (VideoPlayer) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.BeginVertical("box");
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);

        switch(videoPlayer.source)
        {
            case VideoSource.VideoClip:
                if (videoPlayer.clip != null && !videoPlayer.isPlaying && GUILayout.Button("Play"))
                {
                    videoPlayer.Play();
                }
                break;
            case VideoSource.Url:
                if (videoPlayer.isPrepared && !videoPlayer.isPlaying && GUILayout.Button("Play"))
                {
                    videoPlayer.Play();
                }
                break;
        }
        

        if (videoPlayer.isPlaying && GUILayout.Button("Pause"))
        {
            videoPlayer.Pause();
        }

        if (videoPlayer.isPlaying && GUILayout.Button("Stop"))
        {
            videoPlayer.Stop();
        }

        GUILayout.Space(10);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(videoPlayer);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
