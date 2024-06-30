using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;
using TMPro;

public class TimerInPeriod : MonoBehaviour
{
    public TextMeshPro timerText;

    [Range(0.0f, 3600.0f)]
    public float seconds = 0.0f;

    [Range(1.0f, 3600.0f)]
    public float period = 1.0f;

    public bool flashing;

    public UnityEvent onTimerFinishEvent;

    private readonly float flashingWait = 0.1f;

    public void TimeUp()
    {
        StartCoroutine(Timing(seconds, period, flashing, true));
    }

    public void TimeDown()
    {
        StartCoroutine(Timing(seconds, period, flashing, false));
    }

    IEnumerator Timing(float seconds, float period, bool flashing, bool timingUp)
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(0);

        float step;
        int steps;
        float wait;

        if (flashing)
        {
            wait = flashingWait;
            steps = (int)(period / wait);
            step = seconds / steps;   
        }
        else
        {
            step = seconds / period;
            steps = (int)(seconds / step);
            wait = period / steps;
        }

        if(timingUp)
        {
            for (int i = 0; i <= steps; i++)
            {
                UpdateText(step * i, timeSpan);
                yield return new WaitForSeconds(wait);
            }
        }
        else
        {
            for (int i = steps; i >= 0; i--)
            {
                UpdateText(step * i, timeSpan);
                yield return new WaitForSeconds(wait);
            }
        }

        if(onTimerFinishEvent != null)
        {
            onTimerFinishEvent.Invoke();
        }
    }

    private void UpdateText(float seconds, TimeSpan timeSpan)
    {
        timeSpan = TimeSpan.FromSeconds(seconds);
        timerText.text = string.Format("{0:00}:{1:00}", timeSpan.Minutes, timeSpan.Seconds);
    }
}
