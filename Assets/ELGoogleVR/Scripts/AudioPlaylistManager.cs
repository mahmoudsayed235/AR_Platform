using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class AudioPlaylistManager : MonoBehaviour
{
    public AudioClip[] audioClips;
    public UnityEvent[] onAudioClipFinishAcions;
    public bool looping;

    private AudioSource audioSource;
    private int currentClip;

    void Awake ()
    {
        audioSource = GetComponent<AudioSource>();
	}

    private void OnEnable()
    {
        currentClip = -1;
        NextClip();
    }

    public void NextClip()
    {
        if(currentClip >= 0 && currentClip < onAudioClipFinishAcions.Length && onAudioClipFinishAcions[currentClip] != null)
        {
            //Debug.LogFormat("Audio Playlist: Invoking finish actions for audio clip: {0}", currentClip + 1);
            onAudioClipFinishAcions[currentClip].Invoke();
        }

        currentClip++;

        if(currentClip >= audioClips.Length)
        {
            if(looping)
            {
                currentClip = -1;
                //Debug.LogFormat("Audio Playlist: Looping");
                NextClip();
                return;
            }
            
            //Debug.LogFormat("Audio Playlist: No more audio clips");
            return;
        }

        //Debug.LogFormat("Audio Playlist: Playing audio clip: {0}", currentClip + 1);
        audioSource.clip = audioClips[currentClip];
        audioSource.Play();
    }
}
