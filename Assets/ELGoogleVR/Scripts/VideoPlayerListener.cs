using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerListener : MonoBehaviour
{
    public bool playOnUnPause;

    private VideoPlayer videoPlayer;
    private bool paused;

    void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
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
        if (pause && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            paused = true;
        }

        if (!pause && (paused || playOnUnPause))
        {
            videoPlayer.Play();
            paused = false;
            playOnUnPause = false;
        }
    }
}
