using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoPlayerController : MonoBehaviour
{
    //Video To Play [Assign from the Editor]
    public string videoName;

    public bool playOnStart;

    private VideoPlayer videoPlayer;

    public VideoPlayer VideoPlayer { get { return videoPlayer; } }

    private bool isPrepared;

    private void Awake()
    {
        //Add VideoPlayer to the GameObject
        videoPlayer = GetComponent<VideoPlayer>();

        StartCoroutine(PreparingVideo());
    }

    // Use this for initialization
    void Start()
    {
        if(playOnStart)
        {
            Play();
        }
    }

    public void Play()
    {
        StartCoroutine(PlayingVideo());
    }

    IEnumerator PreparingVideo()
    {
        // We want to play from video clip not from url
        videoPlayer.source = VideoSource.Url;

        /////streaming asset folder/////
        //videoPlayer.url= Application.streamingAssetsPath + "/" + videoName + ".mp4";
        //videoPlayer.url="jar:file://" + Application.dataPath + "!/assets/" +videoName+".mp4";
        ///////////

        //persistent Data Path folder
        videoPlayer.url = Application.persistentDataPath + "/" + videoName + ".mp4";

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1);

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            //Debug.Log("Not Prapared Yet @ PreparingVideo");
            yield return waitTime;
        }

        //Debug.Log("Done Preparing Video");
    }

    IEnumerator PlayingVideo()
    {
        while (!videoPlayer.isPrepared)
        {
            //Debug.Log("Not Prapared Yet @ PlayingVideo");
            yield return new WaitForEndOfFrame();
        }

        videoPlayer.Play();
    }
}
