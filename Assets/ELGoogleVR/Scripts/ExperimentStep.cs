using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ExperimentStep : MonoBehaviour
{
    [Range(0.0f, 60.0f)]
    public float beginDelay;
    public UnityEvent onBeginEvent;

    [Range(0.0f, 60.0f)]
    public float finishDelay;
    public UnityEvent onFinishEvent;

    public void Begin()
    {
        Invoke("InvokeBegin", beginDelay);
    }

    public void Finish()
    {
        Invoke("InvokeFinish", finishDelay);
    }

    private void InvokeBegin()
    {
        if (onBeginEvent != null)
        {
            onBeginEvent.Invoke();
        }
    }

    private void InvokeFinish()
    {
        if (onFinishEvent != null)
        {
            onFinishEvent.Invoke();
        }
    }
}
