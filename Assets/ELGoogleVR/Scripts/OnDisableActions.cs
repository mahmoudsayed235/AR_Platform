using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnDisableActions : MonoBehaviour
{
    public float delay;
    public UnityEvent actions;

    private void OnDisable()
    {
        Invoke("InvokeActions", delay);
    }

    private void InvokeActions()
    {
        if (actions != null)
        {
            actions.Invoke();
        }
    }
}
