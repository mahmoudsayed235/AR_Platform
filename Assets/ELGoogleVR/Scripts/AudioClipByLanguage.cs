using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClipByLanguage : MonoBehaviour
{
    public AudioClip[] audioClips;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

		int language = PlayerPrefs.GetInt(PlayerPrefsKeys.Language, 0);

        if (language >= audioClips.Length)
        {
            //Debug.LogFormat("Unavailable Audio Clip for Specified Language");
            return;
        }

        audioSource.clip = audioClips[language];
    }
}
