using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateOnUnpauseListener : MonoBehaviour
{
    public GameObject target;
    public bool autoDisable = true;

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
        if (!pause && target != null && !target.activeInHierarchy)
        {
            target.SetActive(true);
            gameObject.SetActive(!autoDisable);
        }
    }
}
