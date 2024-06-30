using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Choice
{
    public int id;
    public bool isCorrect;
    public string text;
    public Sprite sprite;
    public AudioClip audio;
}
