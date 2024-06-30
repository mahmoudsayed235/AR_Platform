using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPrefsEntry
{
    public enum Type { String, Integer, Float }

    public Type type;
    public string Key;
    public string value;
}
