using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using EnjoyLearning.VR.SDK;
//using UnityEditor;
//using System.Linq;

public class SceneLoaderWithOptions : MonoBehaviour
{
    public enum LoadMode { VR = 0, AR = 1, Normal = 2, Classroom = 3 }

    public bool scenesFromBuildSetting;
    public int scenesToEscapeFromOptions = 2;

    public bool availableVR = true;
    public bool availableNormal = true;
    public bool availableAR = true;
    public bool availableClassroom = true;

    public string classroomLobbySceneName;
    public string jsonQuestions;
    public Dropdown dropdownSceneName;
    public Toggle toggleNormalClassroom;

    public Toggle toggleCanHost;
    public Toggle toggleSFX;
    public InputField inputFieldUserName;
    public InputField inputFieldSimulationId;
    public InputField inputField360VideoName;

    public Button buttonVR;
    public Button buttonNormal;
    public Button buttonAR;
    public Button buttonClassRoom;
    
    private void Awake()
    {
        if(scenesFromBuildSetting)
        {
            int sceneCount = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
            string[] scenes = new string[sceneCount];
            for (int i = scenesToEscapeFromOptions; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i));
            }

            if(scenes.Length > scenesToEscapeFromOptions)
            {
                List<Dropdown.OptionData> scenesOptions = new List<Dropdown.OptionData>(scenes.Length - scenesToEscapeFromOptions);
                for (int i = 0; i < scenesOptions.Capacity; i++)
                {
                    scenesOptions.Add(new Dropdown.OptionData());
                    scenesOptions[i].text = scenes[i + scenesToEscapeFromOptions];
                }

                dropdownSceneName.AddOptions(scenesOptions);
            }
        }

        dropdownSceneName.enabled = dropdownSceneName.options.Count > 0;

        toggleCanHost.isOn = PlayerPrefs.GetInt(PlayerPrefsKeys.UserCanHost, 0) == 1;
        toggleSFX.isOn = PlayerPrefs.GetInt(PlayerPrefsKeys.SFX, 0) == 1;
        inputFieldUserName.text = PlayerPrefs.GetString(PlayerPrefsKeys.UserName, "");
        toggleNormalClassroom.isOn = PlayerPrefs.GetString(PlayerPrefsKeys.VRMode) == PlayerPrefsValues.VRModeNoraml;
        inputFieldSimulationId.text = PlayerPrefs.GetInt(PlayerPrefsKeys.SimulationID, 0).ToString();
        inputField360VideoName.text = PlayerPrefs.GetString(PlayerPrefsKeys.Video360Name, "");

        buttonVR.interactable = availableVR;
        buttonNormal.interactable = availableNormal;
        buttonAR.interactable = availableAR;
        buttonClassRoom.interactable = availableClassroom;
        toggleNormalClassroom.interactable = availableClassroom;

        dropdownSceneName.value = PlayerPrefs.GetInt("SelectedDropdownSceneName", 0);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
    }

    public void LoadVRScene()
    {
        //Debug.LogFormat("SceneLoaderWithOptions -> {0} in with mode: {1}", dropdownSceneName.captionText.tex, "VR");

        PlayerPrefs.SetInt(PlayerPrefsKeys.VirtualClassroom, 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.VRMode, PlayerPrefsValues.VRModeVR);
        PlayerPrefs.Save();

        SaveOptions();
        StartCoroutine(SceneLoader.LoadSceneWithDevice(dropdownSceneName.captionText.text, true));
    }

    public void LoadNoramlScene()
    {
        //Debug.LogFormat("SceneLoaderWithOptions -> {0} in with mode: {1}", dropdownSceneName.captionText.text, "Normal");

        PlayerPrefs.SetInt(PlayerPrefsKeys.VirtualClassroom, 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.VRMode, PlayerPrefsValues.VRModeNoraml);
        PlayerPrefs.Save();

        SaveOptions();
        StartCoroutine(SceneLoader.LoadSceneWithDevice(dropdownSceneName.captionText.text, false));
    }

    public void LoadARScene()
    {
        //Debug.LogFormat("SceneLoaderWithOptions -> {0} in with mode: {1}", dropdownSceneName.captionText.tex, "AR");

        PlayerPrefs.SetInt(PlayerPrefsKeys.VirtualClassroom, 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.VRMode, PlayerPrefsValues.VRModeNoraml);
        PlayerPrefs.Save();

        SaveOptions();
        StartCoroutine(SceneLoader.LoadSceneWithDevice(dropdownSceneName.captionText.text, false));
    }

    public void LoadClassroomScene()
    {
        //Debug.LogFormat("SceneLoaderWithOptions -> {0} in with mode: {1}", dropdownSceneName.captionText.tex, normalClassroomToggle.isOn ? "Normal" : "VR");

        PlayerPrefs.SetString(PlayerPrefsKeys.ClassRoomScene, dropdownSceneName.captionText.text);

        PlayerPrefs.SetInt(PlayerPrefsKeys.VirtualClassroom, 1);
        PlayerPrefs.SetString(PlayerPrefsKeys.VRMode, toggleNormalClassroom.isOn? PlayerPrefsValues.VRModeNoraml : PlayerPrefsValues.VRModeVR);
        PlayerPrefs.Save();

        SaveOptions();
        StartCoroutine(SceneLoader.LoadSceneWithDevice(classroomLobbySceneName, false));
    }

    private void SaveOptions()
    {
        PlayerPrefs.SetString("SceneToLoad", dropdownSceneName.captionText.text);
        PlayerPrefs.SetInt(PlayerPrefsKeys.UserCanHost, toggleCanHost.isOn ? 1 : 0);
        PlayerPrefs.SetInt(PlayerPrefsKeys.SFX, toggleSFX.isOn ? 1 : 0);
        PlayerPrefs.SetString(PlayerPrefsKeys.UserName, inputFieldUserName.text);
        PlayerPrefs.SetString(PlayerPrefsKeys.Video360Name, inputField360VideoName.text);
        PlayerPrefs.SetInt(PlayerPrefsKeys.SimulationID, int.Parse(inputFieldSimulationId.text));
        PlayerPrefs.SetString(PlayerPrefsKeys.Questions, jsonQuestions);
        PlayerPrefs.SetInt("SelectedDropdownSceneName", dropdownSceneName.value);
        PlayerPrefs.Save();

        //Debug.LogFormat("SceneLoaderWithOptions -> Audio Listener Volume: {0}", AudioListener.volume);
    }
}
