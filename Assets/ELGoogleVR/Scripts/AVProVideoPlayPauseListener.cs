using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

[RequireComponent(typeof(MediaPlayer))]
public class AVProVideoPlayPauseListener : MonoBehaviour
{
    public bool playOnUnPause;

    private MediaPlayer mediaPlayer;
    private bool paused;

    void Awake()
    {
        mediaPlayer = GetComponent<MediaPlayer>();
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
        if (pause && !mediaPlayer.Control.IsFinished() && mediaPlayer.Control.IsPlaying())
        {
            mediaPlayer.Pause();
            paused = true;
        }

        if (!pause && (paused || playOnUnPause))
        {
            mediaPlayer.Play();
            paused = false;
            playOnUnPause = false;
        }
    }
}
