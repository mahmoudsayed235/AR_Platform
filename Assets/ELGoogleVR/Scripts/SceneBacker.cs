using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EnjoyLearning.VR.SDK
{
    public class SceneBacker : MonoBehaviour
    {
        public string SceneName;

        private string[] scenes;

        private void Start()
        {
            int sceneCount = SceneManager.sceneCountInBuildSettings;
            scenes = new string[sceneCount];
            for (int i = 0; i < sceneCount; i++)
            {
                scenes[i] = System.IO.Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));
                //Debug.Log(scenes[i]);
            }
        }

        void Update()
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                foreach (string scene in scenes)
                {
                    if (SceneName == scene)
                    {
                        SceneManager.LoadScene(SceneName);
                    }
                }

                Application.Quit();
            }
        }
    }
}

