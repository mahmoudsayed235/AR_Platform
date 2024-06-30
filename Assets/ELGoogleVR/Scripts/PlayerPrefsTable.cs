using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerPrefsTable : ScriptableObject
{
    public List<PlayerPrefsEntry> prefs = new List<PlayerPrefsEntry>();
}
