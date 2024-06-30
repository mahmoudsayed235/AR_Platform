using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

[CustomEditor(typeof(Question))]
public class QuestionEditor : Editor
{
    private Question question;

    private void OnEnable()
    {
        question = (Question) target;
    }

    public override void OnInspectorGUI()
    {
        // begin box 1
        EditorGUILayout.BeginVertical("box");
        GUILayout.Space(5);

        // begin box 2
        EditorGUILayout.BeginVertical("box");
        GUILayout.Space(5);

        // begin row 1
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);

        GUILayout.Label("ID", GUILayout.Width(64));
        GUILayout.Space(5);
        int.TryParse(EditorGUILayout.TextField(question.id.ToString()), out question.id);

        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
        // end row 1

        GUILayout.Space(5);

        // begin row 2
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);

        question.kind = (Question.Kind)EditorGUILayout.EnumPopup(question.kind);
        
        if (question.kind == Question.Kind.TrueFalse)
        {
            GUILayout.Space(10);
            question.isCorrect = EditorGUILayout.Toggle(question.isCorrect, GUILayout.Width(16));
        }
        
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
        // end row 2

        GUILayout.Space(5);

        // begin row 3
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);

        question.type = (Question.Type)EditorGUILayout.EnumPopup(question.type, GUILayout.Width(64));
        GUILayout.Space(5);

        if (question.type == Question.Type.Text)
        {
            question.questionText = EditorGUILayout.TextField(question.questionText);
        }

        if (question.type == Question.Type.Sprite)
        {
            question.questionSprite = (Sprite) EditorGUILayout.ObjectField(question.questionSprite, typeof(Sprite), false);
        }
        
        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
        // end row 3

        GUILayout.Space(5);

        // begin row 4
        EditorGUILayout.BeginHorizontal();
        GUILayout.Space(5);

        question.audio = (AudioClip)EditorGUILayout.ObjectField(question.audio, typeof(AudioClip), false);

        GUILayout.Space(5);
        EditorGUILayout.EndHorizontal();
        // end row 4

        GUILayout.Space(5);
        EditorGUILayout.EndVertical();
        // end box 2

        if(question.kind == Question.Kind.MultipleChoice)
        {
            // begin box 3
            EditorGUILayout.BeginVertical("box");
            GUILayout.Space(5);

            // begin row 1
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(5);

            GUILayout.Label(string.Format("Choices: {0}", question.choices.Count));

            GUILayout.Space(10);

            if (GUILayout.Button("Add Choice"))
            {
                question.choices.Add(new Choice());
            }

            GUILayout.Space(5);
            EditorGUILayout.EndHorizontal();
            // end row 1

            if (question.choices.Count > 0)
            {
                // begin row 2
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(5);

                // begin box 4
                EditorGUILayout.BeginVertical("box");
                GUILayout.Space(5);

                for (int i = 0; i < question.choices.Count; i++)
                {
                    // begin box
                    EditorGUILayout.BeginVertical("box");
                    GUILayout.Space(5);

                    // begin row 1
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(5);

                    GUILayout.Label("ID");
                    GUILayout.Space(10);
                    int.TryParse(EditorGUILayout.TextField(question.choices[i].id.ToString()), out question.choices[i].id);

                    GUILayout.Space(10);
                    question.choices[i].isCorrect = EditorGUILayout.Toggle(question.choices[i].isCorrect, GUILayout.Width(16));

                    GUILayout.Space(10);
                    if (GUILayout.Button("X"))
                    {
                        question.choices.RemoveAt(i);
                        return;
                    }

                    GUILayout.Space(5);
                    EditorGUILayout.EndHorizontal();
                    // end row 1

                    GUILayout.Space(5);

                    // begin row 2
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(5);

                    if (question.type == Question.Type.Text)
                    {
                        question.choices[i].text = EditorGUILayout.TextField(question.questionText);
                    }

                    if (question.type == Question.Type.Sprite)
                    {
                        question.choices[i].sprite = (Sprite)EditorGUILayout.ObjectField(question.choices[i].sprite, typeof(Sprite), false);
                    }

                    GUILayout.Space(5);
                    EditorGUILayout.EndHorizontal();
                    // end row 2

                    GUILayout.Space(5);

                    // begin row 3
                    EditorGUILayout.BeginHorizontal();
                    GUILayout.Space(5);

                    question.choices[i].audio = (AudioClip)EditorGUILayout.ObjectField(question.choices[i].audio, typeof(AudioClip), false);

                    GUILayout.Space(5);
                    EditorGUILayout.EndHorizontal();
                    // end row 3

                    GUILayout.Space(5);
                    EditorGUILayout.EndVertical();
                    // end box
                }

                GUILayout.Space(5);
                EditorGUILayout.EndVertical();
                // end box 4

                GUILayout.Space(5);
                EditorGUILayout.EndHorizontal();
                // end row 2
            }
            
            GUILayout.Space(5);
            EditorGUILayout.EndVertical();
            // end box 3
        }

        GUILayout.Space(5);
        EditorGUILayout.EndVertical();
        // end box 1

        if (GUI.changed)
        {
            EditorUtility.SetDirty(question);
            if (!Application.isPlaying)
                EditorSceneManager.MarkSceneDirty(SceneManager.GetActiveScene());
        }
    }
}
