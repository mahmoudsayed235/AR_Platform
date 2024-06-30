using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using EnjoyLearning.VR.SDK;
public class vr360VideoController : MonoBehaviour
{
   

     public void setVideoName(string videoName)
    {
        PlayerPrefs.SetString(PlayerPrefsKeys.Video360Name, videoName);
    }

    
    public void openScene(string modeAndName)
    {
        string[] arr = modeAndName.Split(' ');
        PlayerPrefs.SetString(PlayerPrefsKeys.VRMode, arr[0]);
        PlayerPrefs.SetString(PlayerPrefsKeys.Video360Name, arr[1]);
        print(PlayerPrefs.GetString(PlayerPrefsKeys.VRMode));
        print(PlayerPrefs.GetString(PlayerPrefsKeys.Video360Name));
        if (arr[0] == "normal")
        {
            StartCoroutine(SceneLoader.LoadSceneWithDevice("360 Video Player", false));
        }
        else
        {
            StartCoroutine(SceneLoader.LoadSceneWithDevice("360 Video Player", true));

        }
    }

}
