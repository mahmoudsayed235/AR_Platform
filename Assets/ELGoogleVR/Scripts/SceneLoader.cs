using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;


namespace EnjoyLearning.VR.SDK
{
    public static class SceneLoader
    {
        public static IEnumerator LoadSceneWithDevice(string sceneToLoad, string deviceName)
        {
            XRSettings.LoadDeviceByName(deviceName);

            while (!XRSettings.loadedDeviceName.Equals(deviceName))
            {
                yield return new WaitForEndOfFrame();
            }

            XRSettings.enabled = true;
            
            SceneManager.LoadScene(sceneToLoad);
        }

        public static IEnumerator LoadSceneWithDevice(string sceneToLoad, bool vrMode)
        {
            string deviceName = vrMode ? "cardboard" : "";
            XRSettings.LoadDeviceByName(deviceName);

            while (!XRSettings.loadedDeviceName.Equals(deviceName))
            {
                yield return new WaitForEndOfFrame();
            }

            XRSettings.enabled = vrMode;

            SceneManager.LoadScene(sceneToLoad);
        }

        public static IEnumerator LoadhDevice(bool vrMode)
        {
            string deviceName = vrMode ? "cardboard" : "";
            XRSettings.LoadDeviceByName(deviceName);

            while (!XRSettings.loadedDeviceName.Equals(deviceName))
            {
                yield return new WaitForEndOfFrame();
            }

            XRSettings.enabled = vrMode;
            while(XRSettings.enabled != vrMode)
            {
                XRSettings.enabled = vrMode;
                yield return new WaitForEndOfFrame();
            }
        }
    }
}

