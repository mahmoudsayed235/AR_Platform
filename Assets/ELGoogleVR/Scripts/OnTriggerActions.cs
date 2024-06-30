using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerActions : MonoBehaviour
{
    public UnityEvent triggerEnterActions;
    public UnityEvent triggerStayActions;
    public UnityEvent triggerExitActions;

    void OnTriggerEnter(Collider other)
    {
        if (triggerEnterActions != null)
        {
            triggerEnterActions.Invoke();
        }
    }
    
    void OnTriggerStay(Collider other)
    {
        if (triggerStayActions != null)
        {
            triggerStayActions.Invoke();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (triggerExitActions != null)
        {
            triggerExitActions.Invoke();
        }
    }
}
