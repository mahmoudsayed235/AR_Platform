using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace EnjoyLearning.VR.SDK
{
    public class NormalModeCameraSwitcher : MonoBehaviour
    {
        public GyroCamera gyroCamera;
        public TouchCamera touchCamera;

        public VRModeManager vrModeManager;
        public Toggle gyroCameraToggle;

        private void Awake()
        {
            ToggleGyroCamera(SystemInfo.supportsGyroscope);

            if(gyroCameraToggle != null)
            {
                gyroCameraToggle.interactable  = gyroCameraToggle.isOn = SystemInfo.supportsGyroscope;

                gyroCameraToggle.gameObject.SetActive(!vrModeManager.vrMode);
            }
            
        }

        public void ToggleGyroCamera(bool gyro)
        {
            gyroCamera.enabled = gyro;
            touchCamera.enabled = !gyro;
        }
    }
}

