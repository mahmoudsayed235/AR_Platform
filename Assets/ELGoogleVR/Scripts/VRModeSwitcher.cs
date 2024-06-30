using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

namespace EnjoyLearning.VR.SDK
{
    public class VRModeSwitcher : MonoBehaviour
    {
        public string vrModeKey;
        public string sceneToLoad;

        public void SwitchMode(bool vrModeEnabled)
        {
            PlayerPrefs.SetInt(vrModeKey, vrModeEnabled ? 1 : 0);
            PlayerPrefs.Save();

            //StartCoroutine(SceneLoader.LoadSceneWithDevice(sceneToLoad, vrModeEnabled ? "cardboard" : ""));
            StartCoroutine(SceneLoader.LoadSceneWithDevice(sceneToLoad, vrModeEnabled));
        }
    }
}

