using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;


[CustomEditor(typeof(PlayerPrefsTable))]
public class PlayerPrefsManagerEditor : Editor
{
    private PlayerPrefsTable playerPrefsTable;

    private void OnEnable()
    {
        playerPrefsTable = (PlayerPrefsTable) target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);

        GUILayout.Label(string.Format("Total Entries: {0}", playerPrefsTable.prefs.Count));
        GUILayout.Space(20);

        if (GUILayout.Button("Add Entry"))
        {
            playerPrefsTable.prefs.Add(new PlayerPrefsEntry());
        }

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.EndVertical();

        if(playerPrefsTable.prefs.Count > 0)
        {
            GUILayout.BeginVertical("box");
            GUILayout.Space(5);

            for (int i = 0; i < playerPrefsTable.prefs.Count; i++)
            {
                GUILayout.BeginHorizontal();

                GUILayout.Space(5);
                playerPrefsTable.prefs[i].type = (PlayerPrefsEntry.Type) EditorGUILayout.EnumPopup(playerPrefsTable.prefs[i].type, GUILayout.Width(64));

                GUILayout.Space(5);
                playerPrefsTable.prefs[i].Key = EditorGUILayout.TextField(playerPrefsTable.prefs[i].Key, GUILayout.MinWidth(64));

                GUILayout.Space(5);
                playerPrefsTable.prefs[i].value = EditorGUILayout.TextField(playerPrefsTable.prefs[i].value, GUILayout.MinWidth(64));

                GUILayout.Space(5);
                if (GUILayout.Button("X", GUILayout.Width(32)))
                {
                    playerPrefsTable.prefs.RemoveAt(i);
                    return;
                }

                GUILayout.Space(5);
                GUILayout.EndHorizontal();
            }

            GUILayout.Space(5);
            GUILayout.EndVertical();
        }

        GUILayout.BeginVertical("box");
        GUILayout.Space(5);

        GUILayout.BeginHorizontal();
        GUILayout.Space(5);


        if (GUILayout.Button("Load"))
        {
            for (int i = 0; i < playerPrefsTable.prefs.Count; i++)
            {
                switch(playerPrefsTable.prefs[i].type)
                {
                    case PlayerPrefsEntry.Type.String:
                        playerPrefsTable.prefs[i].value = PlayerPrefs.GetString(playerPrefsTable.prefs[i].Key);
                        break;

                    case PlayerPrefsEntry.Type.Integer:
                        playerPrefsTable.prefs[i].value = PlayerPrefs.GetInt(playerPrefsTable.prefs[i].Key, 0).ToString();
                        break;

                    case PlayerPrefsEntry.Type.Float:
                        playerPrefsTable.prefs[i].value = PlayerPrefs.GetFloat(playerPrefsTable.prefs[i].Key, 0.0f).ToString();
                        break;
                }
            }
        }

        if (GUILayout.Button("Save"))
        {
            int intValue;
            float floatValue;

            for (int i = 0; i < playerPrefsTable.prefs.Count; i++)
            {
                switch (playerPrefsTable.prefs[i].type)
                {
                    case PlayerPrefsEntry.Type.String:
                        PlayerPrefs.SetString(playerPrefsTable.prefs[i].Key, playerPrefsTable.prefs[i].value);
                        break;

                    case PlayerPrefsEntry.Type.Integer:
                        int.TryParse(playerPrefsTable.prefs[i].value, out intValue);
                        PlayerPrefs.SetInt(playerPrefsTable.prefs[i].Key, intValue);
                        break;

                    case PlayerPrefsEntry.Type.Float:
                        float.TryParse(playerPrefsTable.prefs[i].value, out floatValue);
                        PlayerPrefs.SetFloat(playerPrefsTable.prefs[i].Key, floatValue);
                        break;
                }
            }

            PlayerPrefs.Save();
        }

        GUILayout.Space(5);
        GUILayout.EndHorizontal();

        GUILayout.Space(5);
        GUILayout.EndVertical();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(playerPrefsTable);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
