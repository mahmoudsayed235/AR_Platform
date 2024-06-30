using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioListenerMuteUnmuteListener : MonoBehaviour
{
    public bool startMuted;

    void OnEnable()
    {
        MuteUnmuteManager.OnMute += OnMute;
        AudioListener.volume = startMuted ? 0.0f : 1.0f;
    }

    void OnDisable()
    {
        MuteUnmuteManager.OnMute -= OnMute;
    }

    void OnMute(bool muted)
    {
        AudioListener.volume = muted ? 0.0f : 1.0f;
        //Debug.LogFormat("On Mute @ Listener: {0} | AudioListener Active: {1}", muted, AudioListener.volume == 1.0f);
    }
}
