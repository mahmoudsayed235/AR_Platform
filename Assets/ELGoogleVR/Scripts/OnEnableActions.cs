using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class OnEnableActions : MonoBehaviour
{
    public float delay;
    public UnityEvent actions;

    private void OnEnable()
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
