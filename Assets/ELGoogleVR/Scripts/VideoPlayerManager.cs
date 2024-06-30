using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
using System.IO;

public class VideoPlayerManager : MonoBehaviour
{
    public GameObject image;

    //Video To Play [Assign from the Editor]
    public string videoName;
    public GameObject videoSphere;
    public GameObject questions;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio
    private AudioSource audioSource;

    private bool paused;

    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        StartCoroutine(PlayVideo());
    }

    IEnumerator PlayVideo()
    {
        print(Application.persistentDataPath);

        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.Url;

        /////streaming asset folder/////
        //videoPlayer.url= Application.streamingAssetsPath +"/"+videoName+".mp4";
        //videoPlayer.url="jar:file://" + Application.dataPath + "!/assets/"+videoName+".mp4";
        ///////////

        //persistent Data Path folder
        videoPlayer.url = Application.persistentDataPath + "/" + videoName + ".mp4";

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.Prepare();
        WaitForSeconds waitTime = new WaitForSeconds(1);

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            //Debug.Log("Preparing Video");
            yield return waitTime;
            break;
        }

        //Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        // image.texture = videoPlayer.texture;
        image.GetComponent<MeshRenderer>().material.SetTexture(0, videoPlayer.texture);

        //Play Video
        videoPlayer.Play();
        
        //Play Sound
        audioSource.Play();

        if (videoName == "cell")
        {
            audioSource.mute = true;
        }

        //Debug.Log("Playing Video");

        long videoFrames = (long)videoPlayer.frameCount;
        //Debug.LogFormat("Video Frames: {0}", videoFrames);

        while (videoPlayer.frame + 1 < videoFrames)
        {
            //Debug.LogFormat("Video Frame: {0}", videoPlayer.frame);
            yield return null;
        }

        //Debug.Log("Done Playing Video");
        ShowQuestions();
    }

    void ShowQuestions()
    {
        questions.SetActive(true);
        videoSphere.SetActive(false);
    }

    private void OnEnable()
    {
        PlayPauseManager.OnPause += OnPause;
    }

    private void OnDisable()
    {
        PlayPauseManager.OnPause -= OnPause;
    }

    private void OnPause(bool pause)
    {
        if (pause && videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            paused = true;
        }

        if (!pause && paused)
        {
            videoPlayer.Play();
            paused = false;
        }
    }
}
