using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;

[RequireComponent ( typeof ( VideoPlayer ) ) ]
public class VideoPlayerFrameActions : MonoBehaviour
{
    [Range(1, 60)]
    public int frames = 1;
    public UnityEvent onFramesReachedEvent;

    private VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        StartCoroutine(WaitingForFrames());
    }

    IEnumerator WaitingForFrames()
    {
        while(videoPlayer.frame < frames)
        {
            //Debug.LogFormat("WaitingForFrames -> Frame: {0}", videoPlayer.frame);
            yield return new WaitForEndOfFrame();
        }

        if(onFramesReachedEvent != null)
        {
            onFramesReachedEvent.Invoke();
        }
    }
}
