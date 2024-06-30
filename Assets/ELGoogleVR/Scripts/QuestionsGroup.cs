using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsGroup
{
    public enum Language { Arabic = 0, English = 1 }
    public enum LanguageDirection { RTL, LTR }

    public Language language;
    public LanguageDirection direction;
    public List<Question> questions = new List<Question>(); 
}
