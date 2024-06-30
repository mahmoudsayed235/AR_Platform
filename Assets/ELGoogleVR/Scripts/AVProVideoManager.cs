using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RenderHeads.Media.AVProVideo;

[RequireComponent(typeof(MediaPlayer))]
public class AVProVideoManager : MonoBehaviour
{
    [SerializeField] public bool byPlayerPrefs;
    [SerializeField] public string videoName;

    private MediaPlayer mediaPlayer;

    private void Awake()
    {
        mediaPlayer = GetComponent<MediaPlayer>();
        print(PlayerPrefs.GetString(PlayerPrefsKeys.Video360Name));
        mediaPlayer.m_VideoPath = string.Format("{0}.mp4", byPlayerPrefs ? PlayerPrefs.GetString(PlayerPrefsKeys.Video360Name, "Not Video Set") : videoName);
    }

    public IMediaControl GetControl()
    {
        return mediaPlayer.Control;
    }

    public void Play()
    {
        mediaPlayer.Control.Play();
    }

    public void Replay()
    {
        mediaPlayer.Control.Rewind();
        mediaPlayer.Control.Play();
    }

    public void Pause()
    {
        mediaPlayer.Control.Pause();
    }
}
