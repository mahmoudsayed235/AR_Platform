using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(AudioSource))]
public class AudioSourceEditor : Editor
{
    private AudioSource audioSource;

    private void OnEnable()
    {
        audioSource = (AudioSource) target;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        GUILayout.BeginVertical("box");
        GUILayout.Space(10);

        GUILayout.BeginHorizontal();
        GUILayout.Space(10);

        if(audioSource.clip != null && !audioSource.isPlaying && GUILayout.Button("Play"))
        {
            audioSource.Play();
        }
        
        if (audioSource.isPlaying && GUILayout.Button("Pause"))
        {
            audioSource.Pause();
        }
        
        if (audioSource.isPlaying && GUILayout.Button("Stop"))
        {
            audioSource.Stop();
        }

        GUILayout.Space(10);
        GUILayout.EndHorizontal();

        GUILayout.Space(10);
        GUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(audioSource);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
