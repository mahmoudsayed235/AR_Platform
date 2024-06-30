using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioFinishActions : MonoBehaviour
{
    public UnityEvent onPlayAcions;
    public UnityEvent onFinishAcions;
    
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        StartCoroutine(CheckingPlayState());
    }

    IEnumerator CheckingPlayState()
    {
        while(!audioSource.isPlaying)
        {
            yield return new WaitForEndOfFrame();
        }

        //Debug.LogFormat("Calling On Play Actions for Audio Source: {0} & Clip: {1}", audioSource.name, audioSource.clip.name);
        if (onPlayAcions != null)
        {
            onPlayAcions.Invoke();
        }

        // wait untill audiosource time exceed 0
        while(audioSource.time == 0)
        {
            yield return new WaitForEndOfFrame();
        }

        if (gameObject.activeInHierarchy)
        {
            //Debug.LogFormat("Checking finish state of Audio Source: {0} & Clip: {1}", audioSource.name, audioSource.clip.name);
            StartCoroutine(CheckingFinishState());
        }
    }

    IEnumerator CheckingFinishState()
    {
        //yield return new WaitForEndOfFrame();

        while (audioSource.time > 0)
        {
            yield return new WaitForEndOfFrame();
        }

        //Debug.LogFormat("Calling On Finish Actions for Audio Source: {0} & Clip: {1}", audioSource.name, audioSource.clip.name);
        if (onFinishAcions != null)
        {
            onFinishAcions.Invoke();
        }

        if (gameObject.activeInHierarchy)
        {
            //Debug.LogFormat("Checking Play state of Audio Source: {0} & Clip: {1}", audioSource.name, audioSource.clip.name);
            StartCoroutine(CheckingPlayState());
        }
    }
}