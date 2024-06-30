using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class PlayPauseManager : MonoBehaviour
{
    public delegate void Pause(bool pause);
    public static event Pause OnPause;

    public Button playPauseButton;
    public Image playPasueImage;
    public Sprite playSprite;
    public Sprite pauseSprite;
    
    private bool paused;
    
    public void PauseClicked(bool paused)
    {
        //Debug.LogFormat("Pause{0}", paused ? "d" : "");

        UpdateGUI(paused);

        if (OnPause != null)
        {
            OnPause(paused);
        }
    }

    public void Switch()
    {
        //Debug.LogFormat("Switching PlayPause on device: {0}", Network.player.ipAddress);
        PauseClicked(!paused);
    }

    public void ShowPlayPauseButton(bool show)
    {
        playPauseButton.gameObject.SetActive(show);
    }

    public void UpdateGUI(bool paused)
    {
        this.paused = paused;
        playPasueImage.sprite = paused ? playSprite : pauseSprite;
    }
}
