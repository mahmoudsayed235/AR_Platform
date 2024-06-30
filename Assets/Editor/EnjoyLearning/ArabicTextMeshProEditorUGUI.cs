using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using TMPro;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(ArabicTextMeshProUGUI))]
public class ArabicTextMeshProEditorUGUI : Editor
{
    private ArabicTextMeshProUGUI arabicTextMeshProUGUI;

    private void OnEnable()
    {
        arabicTextMeshProUGUI = (ArabicTextMeshProUGUI) target;
        arabicTextMeshProUGUI.TextMeshProUGUI = arabicTextMeshProUGUI.GetComponent<TextMeshProUGUI>();
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
		GUILayout.Space(5);

		GUILayout.BeginHorizontal();
		GUILayout.Space(5);

		GUILayout.Label("Fix", GUILayout.MaxWidth(32));
		arabicTextMeshProUGUI.fix = EditorGUILayout.Toggle(arabicTextMeshProUGUI.fix);

		GUILayout.Space(5);
		GUILayout.EndHorizontal();

		GUILayout.Space(5);
		GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

		GUILayout.Label("Text", GUILayout.MaxWidth(32));
        arabicTextMeshProUGUI.Text = EditorGUILayout.TextArea(arabicTextMeshProUGUI.Text, GUILayout.MinHeight(50), GUILayout.MinWidth(200));

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.EndVertical();

        GUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

		GUILayout.Label("Tashkeel", GUILayout.MaxWidth(128));
        arabicTextMeshProUGUI.tashkeel = EditorGUILayout.Toggle(arabicTextMeshProUGUI.tashkeel, GUILayout.Width(16));

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

		GUILayout.Label("Arabic Numbers", GUILayout.MaxWidth(128));
        arabicTextMeshProUGUI.arabicNumbers = EditorGUILayout.Toggle(arabicTextMeshProUGUI.arabicNumbers, GUILayout.Width(16));

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.EndVertical();

        arabicTextMeshProUGUI.UpdateText();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(arabicTextMeshProUGUI);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
