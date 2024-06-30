using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveInVrMode : MonoBehaviour
{
    private void Awake()
    {
        gameObject.SetActive(PlayerPrefs.GetString(PlayerPrefsKeys.VRMode) != PlayerPrefsValues.VRModeNoraml ? true : false);
    }
}
