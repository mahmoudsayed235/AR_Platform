using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSourceListener : MonoBehaviour
{
    public bool playOnUnPause;

    private AudioSource audioSource;
    private bool paused;

    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnEnable()
    {
        PlayPauseManager.OnPause += OnPause;
    }

    private void OnDisable()
    {
        PlayPauseManager.OnPause -= OnPause;
    }

    private void OnPause(bool pause)
    {
        if (pause && audioSource.isPlaying)
        {
            audioSource.Pause();
            paused = true;
        }

        if (!pause && (paused || playOnUnPause))
        {
            audioSource.Play();
            paused = false;
            playOnUnPause = false;
        }
    }
}
