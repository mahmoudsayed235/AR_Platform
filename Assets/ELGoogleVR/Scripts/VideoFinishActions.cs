using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.Video;

public class VideoFinishActions : MonoBehaviour
{
    public UnityEvent onFinishAcions;

    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += InvokeFinishActions;
    }

    private void InvokeFinishActions(UnityEngine.Video.VideoPlayer vp)
    {
        if (onFinishAcions != null)
        {
            onFinishAcions.Invoke();
        }
    }
}
