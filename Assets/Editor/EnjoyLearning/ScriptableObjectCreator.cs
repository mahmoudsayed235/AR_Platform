using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using System.IO;

public class ScriptableObjectCreator
{
    [MenuItem("Assets/Scriptable Object Creator/Create/Question")]
    public static void CreateQuestionAsset()
    {
        Question question = ScriptableObject.CreateInstance<Question>();

        string assetsFolderPath = "Assets/ELGoogleVR/Data Assets/Questions";

        if (!Directory.Exists(assetsFolderPath))
        {
            Directory.CreateDirectory(assetsFolderPath);
        }

        string assetPath = assetsFolderPath + "/Question.asset";

        if(File.Exists(assetPath))
        {
            Debug.LogError("Question asset with the same name Exists!");
            return;
        }

        AssetDatabase.CreateAsset(question, assetPath);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = question;
    }

    [MenuItem("Assets/Scriptable Object Creator/Create/Player Prefs Table")]
    public static void CreatePlayerPrefsTableAsset()
    {
        PlayerPrefsTable playerPrefsTable = ScriptableObject.CreateInstance<PlayerPrefsTable>();

        string assetsFolderPath = "Assets/Editor";

        if (!Directory.Exists(assetsFolderPath))
        {
            Directory.CreateDirectory(assetsFolderPath);
        }

        string assetPath = assetsFolderPath + "/Player Prefs Table.asset";

        if (File.Exists(assetPath))
        {
            Debug.LogError("Player Prefs Table asset with the same name Exists!");
            return;
        }

        AssetDatabase.CreateAsset(playerPrefsTable, assetPath);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = playerPrefsTable;
    }

    [MenuItem("Assets/Scriptable Object Creator/Create/Text Sprite Audio Block")]
    public static void CreateTextSpriteAudioAsset()
    {
        TextSpriteAudioBlock textSpriteAudioBlock = ScriptableObject.CreateInstance<TextSpriteAudioBlock>();

        string assetsFolderPath = "Assets/ELGoogleVR/Data Assets";

        if (!Directory.Exists(assetsFolderPath))
        {
            Directory.CreateDirectory(assetsFolderPath);
        }

        string assetPath = assetsFolderPath + "/Text Sprite Audio Block.asset";

        if (File.Exists(assetPath))
        {
            Debug.LogError("Text Sprite Audio Block asset with the same name Exists!");
            return;
        }

        AssetDatabase.CreateAsset(textSpriteAudioBlock, assetPath);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = textSpriteAudioBlock;
    }

	[MenuItem("Assets/Scriptable Object Creator/Create/Text Audio Block")]
	public static void CreateTextAudioAsset()
	{
		TextAudioBlock textAudioBlock = ScriptableObject.CreateInstance<TextAudioBlock>();

		string assetsFolderPath = "Assets/ELGoogleVR/Data Assets";

		if (!Directory.Exists(assetsFolderPath))
		{
			Directory.CreateDirectory(assetsFolderPath);
		}

		string assetPath = assetsFolderPath + "/Text Audio Block.asset";

		if (File.Exists(assetPath))
		{
			//Debug.LogError("Text Audio Block asset with the same name Exists!");
			return;
		}

		AssetDatabase.CreateAsset(textAudioBlock, assetPath);
		AssetDatabase.SaveAssets();
		EditorUtility.FocusProjectWindow();
		Selection.activeObject = textAudioBlock;
	}

    [MenuItem("Assets/Scriptable Object Creator/Create/Question System Style")]
    public static void CreateQuestionSystemStyleAsset()
    {
        QuestionSystemStyle questionSystemStyle = ScriptableObject.CreateInstance<QuestionSystemStyle>();

        string assetsFolderPath = "Assets/ELGoogleVR/Data Assets/Questions/Styles";

        if (!Directory.Exists(assetsFolderPath))
        {
            Directory.CreateDirectory(assetsFolderPath);
        }

        string assetPath = assetsFolderPath + "/Question System Style.asset";

        if (File.Exists(assetPath))
        {
            //Debug.LogError("Text Audio Block asset with the same name Exists!");
            return;
        }

        AssetDatabase.CreateAsset(questionSystemStyle, assetPath);
        AssetDatabase.SaveAssets();
        EditorUtility.FocusProjectWindow();
        Selection.activeObject = questionSystemStyle;
    }
}
