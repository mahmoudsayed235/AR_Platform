using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

[RequireComponent(typeof(MediaPlayer))]
public class AVProVideoMuteUnmuteListener : MonoBehaviour
{
    private MediaPlayer mediaPlayer;

    void Awake()
    {
        mediaPlayer = GetComponent<MediaPlayer>();
    }

    void OnEnable()
    {
        MuteUnmuteManager.OnMute += OnMute;
    }

    void OnDisable()
    {
        MuteUnmuteManager.OnMute -= OnMute;
    }

    void OnMute(bool muted)
    {
        mediaPlayer.Control.MuteAudio(muted);

        //Debug.LogFormat("On Mute @ AudioListenerMuteUnmuteListener: {0} | Video Muted: {1}", muted, mediaPlayer.Control.IsMuted());
    }
}
