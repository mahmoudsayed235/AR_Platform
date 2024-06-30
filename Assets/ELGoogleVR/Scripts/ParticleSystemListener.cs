using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemListener : MonoBehaviour
{
    public bool playOnUnPause;

    private ParticleSystem mParticleSystem;
    private bool paused;

    private void Awake()
    {
        mParticleSystem = GetComponent<ParticleSystem>();
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
        if (pause && mParticleSystem.isPlaying)
        {
            mParticleSystem.Pause();
            paused = true;
        }

        if (!pause && (paused || playOnUnPause))
        {
            mParticleSystem.Play();
            paused = false;
        }
    }
}
