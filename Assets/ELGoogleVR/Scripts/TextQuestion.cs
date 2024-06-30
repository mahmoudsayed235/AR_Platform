using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

[System.Serializable]
public class TextQuestion
{
	public enum Type { TrueFalse, MultipleChoice }

	public Type type;

	public int id;

	public string text;

	public float startTime;
	public float endTime;

    public int points;

    public bool shuffle;

    public List<TextChoice> choices = new List<TextChoice>();

	public int GetCorrectChoiceID()
	{
		foreach (TextChoice choice in choices) 
		{
			if (choice.isCorrect) 
			{
				return choice.id;
			}
		}

		//Debug.LogErrorFormat ("Question: {0} has no correct choice", id);
		return -1;
	}

	public bool IsCorrect()
	{
		return choices [0].isCorrect;
	}
}

