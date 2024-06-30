using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    public delegate void TimerOff(string time);
    public static event TimerOff OnTimerOff;

    public bool active { set; get; }
    public bool useMilliseconds;
    [Range(1, 3)]
    public int millisecondsDigits = 1;
    public TextMeshPro timerTextMeshPro;
    public Text timerText;
    public AudioSource sfxTimerFinish;

    private bool timing;
    private TimeSpan timeSpan;
    private bool paused;

    private void Start()
    {
        UpdateText(0);
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
        paused = pause;
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.O))
        {
            active = !active;
            if (active)
                On();
            else
                Off();
        }

    }

    public void On()
    {
        if(active)
        {
            StartCoroutine(Timing());
        }
    }

    public void Off()
    {
        timing = false;
    }

    IEnumerator Timing()
    {
        timing = true;

        TimeSpan timeSpan = TimeSpan.FromSeconds(0);
        float seconds = 0;
        UpdateText(seconds);

        while (timing)
        {
            while(paused)
            {
                yield return new WaitForEndOfFrame();
            }

            yield return new WaitForEndOfFrame();
            seconds += Time.deltaTime;
            UpdateText(seconds);
        }

        if(sfxTimerFinish != null)
        {
            sfxTimerFinish.Play();
        }

        if(OnTimerOff != null)
        {
            string time;
            timeSpan = TimeSpan.FromSeconds(seconds);

            if (useMilliseconds)
            {
                time = string.Format("{0:00}:{1}", timeSpan.Seconds, int.Parse(timeSpan.Milliseconds.ToString().Substring(0, Mathf.Min(timeSpan.Milliseconds.ToString().Length, millisecondsDigits))));
            }
            else
            {
                time = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            }

            OnTimerOff(time);
        }
    }

    private void UpdateText(float seconds)
    {
        timeSpan = TimeSpan.FromSeconds(seconds);

        if(timerTextMeshPro != null)
        {
            if (useMilliseconds)
            {
                timerTextMeshPro.text = string.Format("{0:00}:{1}", timeSpan.Seconds, int.Parse(timeSpan.Milliseconds.ToString().Substring(0, Mathf.Min(timeSpan.Milliseconds.ToString().Length, millisecondsDigits))));
            }
            else
            {
                timerTextMeshPro.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            }
        }

        if (timerText != null)
        {
            if (useMilliseconds)
            {
                timerText.text = string.Format("{0:00}:{1}", timeSpan.Seconds, int.Parse(timeSpan.Milliseconds.ToString().Substring(0, Mathf.Min(timeSpan.Milliseconds.ToString().Length, millisecondsDigits))));
            }
            else
            {
                timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
            }
        }
    }
}

