using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[System.Serializable]
public class Question : ScriptableObject
{
	public enum Kind { TrueFalse, MultipleChoice }
	public enum Type { Sprite, Text }

	public Kind kind;
	public Type type;

	public int id;

	public string questionText;
	public Sprite questionSprite;
	public AudioClip audio;

	public bool isCorrect;
	public List<Choice> choices = new List<Choice>();

	public override string ToString()
	{
		StringBuilder strBuilder = new StringBuilder();

		strBuilder.AppendLine(string.Format("Kind: {0}\n Type: {1}", kind, type));

		if(type == Type.Text)
		{
			strBuilder.Append(string.Format("Question: {0}", questionText));
			for (int i = 1; i < choices.Count; i++)
				strBuilder.Append(string.Format("Choice Id: {0} --> {1}", choices[i].id, choices[i].text));
		}

		strBuilder.AppendLine();

		return strBuilder.ToString();
	}
}
