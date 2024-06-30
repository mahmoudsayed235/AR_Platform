using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorListener : MonoBehaviour
{
    public bool playOnUnPause;

    private Animator animator;
    private bool paused;

    void Awake()
    {
        animator = GetComponent<Animator>();
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
        if (pause && animator.isActiveAndEnabled)
        {
            animator.enabled = false;
            paused = true;
        }

        if (!pause && (paused || playOnUnPause))
        {
            animator.enabled = true;
            paused = false;
        }
    }
}
