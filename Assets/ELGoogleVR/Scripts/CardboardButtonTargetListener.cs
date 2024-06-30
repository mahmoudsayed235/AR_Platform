using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnjoyLearning.VR.SDK;

[RequireComponent(typeof(CardboardButtonTargetActivator))]
public class CardboardButtonTargetListener : MonoBehaviour
{
    public bool activeOnUnPause;

    private CardboardButtonTargetActivator mCardboardButtonTargetActivator;
    private bool paused;

    void Awake()
    {
        mCardboardButtonTargetActivator = GetComponent<CardboardButtonTargetActivator>();
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
        if (pause && mCardboardButtonTargetActivator.IsActive())
        {
            mCardboardButtonTargetActivator.Deactivate();
            paused = true;
        }

        if (!pause && (paused || activeOnUnPause))
        {
            mCardboardButtonTargetActivator.Activate();
            paused = false;
            activeOnUnPause = false;
        }
    }
}
