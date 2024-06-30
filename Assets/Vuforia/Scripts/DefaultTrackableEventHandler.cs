/*==============================================================================
Copyright (c) 2019 PTC Inc. All Rights Reserved.

Copyright (c) 2010-2014 Qualcomm Connected Experiences, Inc.
All Rights Reserved.
Confidential and Proprietary - Protected under copyright and other laws.
==============================================================================*/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using Vuforia;
using System.IO;
using UnityEngine.XR;
using System.Collections;
[System.Serializable]
public class Response
{
    public bool status;
    public string expirationDate;
    public string os;
    public List<Data> data;
}
[System.Serializable]
public class Data
{
    public string name;
    public string triggerName;
    public string url;
    public string timeStamp;
    public string type;
}

/// <summary>
/// A custom handler that implements the ITrackableEventHandler interface.
///
/// Changes made to this file could be overwritten when upgrading the Vuforia version.
/// When implementing custom event handler behavior, consider inheriting from this class instead.
/// </summary>
public class DefaultTrackableEventHandler : MonoBehaviour, ITrackableEventHandler
{
    Response res;
    #region PROTECTED_MEMBER_VARIABLES

    protected TrackableBehaviour mTrackableBehaviour;
    protected TrackableBehaviour.Status m_PreviousStatus;
    protected TrackableBehaviour.Status m_NewStatus;

    #endregion // PROTECTED_MEMBER_VARIABLES

    #region UNITY_MONOBEHAVIOUR_METHODS

    protected virtual void Start()
    {
        mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (mTrackableBehaviour)
            mTrackableBehaviour.RegisterTrackableEventHandler(this);
    }

    protected virtual void OnDestroy()
    {
        if (mTrackableBehaviour)
            mTrackableBehaviour.UnregisterTrackableEventHandler(this);
    }

    #endregion // UNITY_MONOBEHAVIOUR_METHODS

    #region PUBLIC_METHODS

    /// <summary>
    ///     Implementation of the ITrackableEventHandler function called when the
    ///     tracking state changes.
    /// </summary>
    public void OnTrackableStateChanged(
        TrackableBehaviour.Status previousStatus,
        TrackableBehaviour.Status newStatus)
    {
        m_PreviousStatus = previousStatus;
        m_NewStatus = newStatus;

        Debug.Log("Trackable " + mTrackableBehaviour.TrackableName +
                  " " + mTrackableBehaviour.CurrentStatus +
                  " -- " + mTrackableBehaviour.CurrentStatusInfo);

        if (newStatus == TrackableBehaviour.Status.DETECTED ||
            newStatus == TrackableBehaviour.Status.TRACKED ||
            newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
        {
            OnTrackingFound();
        }
        else if (previousStatus == TrackableBehaviour.Status.TRACKED &&
                 newStatus == TrackableBehaviour.Status.NO_POSE)
        {
            OnTrackingLost();
        }
        else
        {
            // For combo of previousStatus=UNKNOWN + newStatus=UNKNOWN|NOT_FOUND
            // Vuforia is starting, but tracking has not been lost or found yet
            // Call OnTrackingLost() to hide the augmentations
            OnTrackingLost();
        }
    }

    #endregion // PUBLIC_METHODS

    #region PROTECTED_METHODS

    protected virtual void OnTrackingFound()
    {
        if (mTrackableBehaviour)
        {
            detectTarget(this.gameObject.name);
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);
            var sounds = GetComponentsInChildren<AudioSource>(true);
            var animators = GetComponentsInChildren<Animator>(true);

            // Enable rendering:
            foreach (var component in rendererComponents)
                component.enabled = true;

            // Enable colliders:
            foreach (var component in colliderComponents)
                component.enabled = true;

            // Enable canvas':
            foreach (var component in canvasComponents)
                component.enabled = true;

            //Enable animator:
            foreach (var animator in animators)
                animator.enabled = true;
            //Enable Sounds:
            foreach (var sound in sounds)
                sound.Play();
        }
    }


    protected virtual void OnTrackingLost()
    {
        if (mTrackableBehaviour)
        {
            var rendererComponents = mTrackableBehaviour.GetComponentsInChildren<Renderer>(true);
            var colliderComponents = mTrackableBehaviour.GetComponentsInChildren<Collider>(true);
            var canvasComponents = mTrackableBehaviour.GetComponentsInChildren<Canvas>(true);

            var sounds = GetComponentsInChildren<AudioSource>(true);
            var animators = GetComponentsInChildren<Animator>(true);
            // Disable rendering:
            foreach (var component in rendererComponents)
                component.enabled = false;

            // Disable colliders:
            foreach (var component in colliderComponents)
                component.enabled = false;

            // Disable canvas':
            foreach (var component in canvasComponents)
                component.enabled = false;

            //Enable animator:
            foreach (var animator in animators)
                animator.enabled = false;
            //Enable Sounds:
            foreach (var sound in sounds)
                sound.Pause();
        }
    }

    AssetBundle arc;
    void detectTarget(string triggerName)
    {

        res = JsonUtility.FromJson<Response>(PlayerPrefs.GetString("THResponse" + PlayerPrefs.GetString("THClientID")));
        print(triggerName);
        foreach (Data data in res.data)
        {
            if (triggerName == data.triggerName)
            {

                switch (data.type)
                {
                    case "2":
                        print("video");
                        PlayerPrefs.SetString("THVideoName", data.name);
                        SceneManager.LoadScene("videoPlayerScene2d");
                        break;
                    case "3":
                        print("video");
                        PlayerPrefs.SetString("THVideoName", data.name);

                        StartCoroutine(SceneLoader.LoadSceneWithDevice("videoPlayerScene360", true));
                        // SceneManager.LoadScene("videoPlayerScene360");
                        break;
                    case "0":
                        print("AR");
                        if (this.gameObject.transform.childCount == 0)
                        {
                            print("create prefab first time");
                            print("stored response : " + PlayerPrefs.GetString("THResponse" + PlayerPrefs.GetString("THClientID")));
                            print("data.name : " + data.name);




                            arc = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, data.name));
                            var prefab = arc.LoadAsset(data.name);
                            GameObject newChild = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
                            newChild.transform.SetParent(this.gameObject.transform);
                            /* newChild.transform.localPosition = new Vector3(-0.088217f, 0.452f, 0.068754f);
                             newChild.transform.localScale = new Vector3(0.344f, 0.344f, 0.344f);
                             newChild.transform.localEulerAngles = new Vector3(90, 0, 0);
                             */
                            newChild.transform.localPosition = Vector3.zero;
                            newChild.transform.localScale = Vector3.one;
                            newChild.transform.localEulerAngles = Vector3.zero;
                            arc.Unload(false);
                        }
                        break;
                    case "1":
                        print("scene");
                        print(Application.persistentDataPath);

                        //  AssetBundle arcScene = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, "interactivesceneeyeenglish"));
                        // SceneManager.LoadScene("interactivesceneeyeenglish");
                        //  StartCoroutine(SceneLoader.loadSceneAsync(data.name));
                        AssetBundle arcScene = AssetBundle.LoadFromFile(Path.Combine(Application.persistentDataPath, data.name));
                        SceneManager.LoadScene(data.name);
                        break;

                    default:
                        break;

                }


                break;
            }
        }

    }



    #endregion // PROTECTED_METHODS
}
public static class SceneLoader
{

    public static IEnumerator loadSceneAsync(string name)
    {
        var arcScene = AssetBundle.LoadFromFileAsync(Path.Combine(Application.persistentDataPath, name));

        yield return arcScene;

        AssetBundle myLoadedAssetBundle = arcScene.assetBundle;
        SceneManager.LoadScene(name);

    }
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
        while (XRSettings.enabled != vrMode)
        {
            XRSettings.enabled = vrMode;
            yield return new WaitForEndOfFrame();
        }
    }


}
