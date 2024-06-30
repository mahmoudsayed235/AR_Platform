using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;
using TMPro;

[CustomEditor(typeof(ArabicTextMeshPro))]
public class ArabicTextMeshProEditor : Editor
{
    private ArabicTextMeshPro arabicTextMeshPro;

    private void OnEnable()
    {
        arabicTextMeshPro = (ArabicTextMeshPro) target;
        arabicTextMeshPro.TextMeshPro = arabicTextMeshPro.GetComponent<TextMeshPro>();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
		GUILayout.Space(5);

		GUILayout.BeginHorizontal();
		GUILayout.Space(5);

		GUILayout.Label("Fix", GUILayout.MaxWidth(32));
		arabicTextMeshPro.fix = EditorGUILayout.Toggle(arabicTextMeshPro.fix);

		GUILayout.Space(5);
		GUILayout.EndHorizontal();

		GUILayout.Space(5);
		GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

		GUILayout.Label("Text", GUILayout.MaxWidth(32));
        arabicTextMeshPro.Text = EditorGUILayout.TextArea(arabicTextMeshPro.Text, GUILayout.MinHeight(50), GUILayout.MinWidth(200));

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

		GUILayout.Label("Tashkeel", GUILayout.MaxWidth(128));
        arabicTextMeshPro.tashkeel = EditorGUILayout.Toggle(arabicTextMeshPro.tashkeel, GUILayout.Width(16));

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

		GUILayout.Label("Arabic Numbers", GUILayout.MaxWidth(128));
        arabicTextMeshPro.arabicNumbers = EditorGUILayout.Toggle(arabicTextMeshPro.arabicNumbers, GUILayout.Width(16));

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.EndVertical();
        
        arabicTextMeshPro.UpdateText();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(arabicTextMeshPro);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
