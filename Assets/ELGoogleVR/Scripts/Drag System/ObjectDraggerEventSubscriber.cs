using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDraggerEventSubscriber : MonoBehaviour
{
    public UnityEvent onDragEvent;
    public UnityEvent onReleaseEvent;

    private void OnEnable()
    {
        ObjectDragger.OnObjectDrag += OnObjectDrag;
        ObjectDragger.OnObjectRelease += OnObjectRelease;
    }

    private void OnDisable()
    {
        ObjectDragger.OnObjectDrag -= OnObjectDrag;
        ObjectDragger.OnObjectRelease -= OnObjectRelease;
    }

    private void OnObjectDrag()
    {
        if(onDragEvent != null)
        {
            onDragEvent.Invoke();
        }
    }

    private void OnObjectRelease()
    {
        if(onReleaseEvent != null)
        {
            onReleaseEvent.Invoke();
        }
    }
}
