using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class MuteUnmuteManager : MonoBehaviour
{
    public delegate void Mute(bool mute);
    public static event Mute OnMute;

    public Button muteUnmuteButton;
    public Image muteUnmuteImage;
    public Sprite muteSprite;
    public Sprite unmuteSprite;
    
    private bool muted = false;
    
    public void MuteClicked(bool muted)
    {
        //Debug.LogFormat("Muted{0}", muted ? "d" : "");

        UpdateGUI(muted);
        
        if (OnMute != null)
        {
            OnMute(muted);
        }
    }

    public void Switch()
    {
        //Debug.LogFormat("Switching MuteUnmute on device: {0}", Network.player.ipAddress);
        MuteClicked(!muted);
    }

    public void ShowMuteUnmuteButton(bool show)
    {
        muteUnmuteButton.gameObject.SetActive(show);
    }

    public void UpdateGUI(bool muted)
    {
        this.muted = muted;
        muteUnmuteImage.sprite = muted ? unmuteSprite : muteSprite;
    }
}
