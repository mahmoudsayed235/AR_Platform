using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnjoyLearning.VR.SDK;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class EventTriggerListener : MonoBehaviour
{
    public bool activeOnUnPause;

    private EventTrigger eventTrigger;
    private bool paused;

    void Awake()
    {
        eventTrigger = GetComponent<EventTrigger>();
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
        if (pause && eventTrigger.enabled)
        {
            eventTrigger.enabled = false;
            paused = true;
        }

        if (!pause && (paused || activeOnUnPause))
        {
            eventTrigger.enabled = true;
            paused = false;
            activeOnUnPause = false;
        }
    }
}
