using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.SceneManagement;
using EnjoyLearning.VR.SDK;
using UnityEngine.Events;

namespace EnjoyLearning.VR.SDK
{
    public class VRModeManager : MonoBehaviour
    {
        public string backScene;
        public bool vrMode;
        public bool virtualClassroom;
        public bool resetAudioListenerOnBack = true;
        public AudioSource backAudio;

        public ScreenOrientation normalModeScreenOrientation;

        public UnityEvent normalModeEvents;
        public UnityEvent vrModeEvents;

        void Awake()
        {
            vrMode = PlayerPrefs.GetString(PlayerPrefsKeys.VRMode) != PlayerPrefsValues.VRModeNoraml ? true : false;
          	
            if (!vrMode)
            {
                Screen.orientation = normalModeScreenOrientation;
			}

			this.transform.GetChild(0).GetComponent<TouchCamera>().enabled = !vrMode;

            if (vrMode)
            {
                if (vrModeEvents != null)
                {
                    vrModeEvents.Invoke();
                }
            }
            else
            {
                if (normalModeEvents != null)
                {
                    normalModeEvents.Invoke();
                }
            }
        }

		private void Update()
		{
			if(Input.GetKeyUp(KeyCode.Escape))
			{
				Back(false);
			}
		}

		public void Back(bool playSound)
		{
            if (playSound && PlayerPrefs.GetInt(PlayerPrefsKeys.SFX) == 1)
            {
                backAudio.Play();
            }

            if(resetAudioListenerOnBack)
                AudioListener.volume = 1;

            if (virtualClassroom)
            {
                //Debug.LogFormat("ServerReturnToLobby by VRModeManager back");
                ReturnToLoby();
                return;
            }

			StartCoroutine(SceneLoader.LoadSceneWithDevice(backScene, ""));
		}

        void ReturnToLoby()
        {
            Screen.sleepTimeout = SleepTimeout.SystemSetting;
        }
    }
}
