using RenderHeads.Media.AVProVideo;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent (typeof(MediaPlayer))]
public class AVProEventsHandler : MonoBehaviour
{
    public UnityEvent onFinishEvents;

    private MediaPlayer mediaPlayer;

    private void Awake()
    {
        mediaPlayer = this.GetComponent<MediaPlayer>();
        mediaPlayer.Events.AddListener(OnVideoEvent);
    }

    public void OnVideoEvent(MediaPlayer mp, MediaPlayerEvent.EventType et, ErrorCode errorCode)
    {
        //Debug.LogFormat("AVProEventsHandler -> {0}", et);

        switch (et)
        {
            case MediaPlayerEvent.EventType.FinishedPlaying:
                if(onFinishEvents != null)
                {
                    onFinishEvents.Invoke();
                }
                break;
        }
    }
}
