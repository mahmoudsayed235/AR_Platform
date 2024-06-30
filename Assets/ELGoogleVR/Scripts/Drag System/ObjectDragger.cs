using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ObjectDragger : MonoBehaviour
{
    public delegate void ObjectDrag();
    public static event ObjectDrag OnObjectDrag;

    public delegate void ObjectRelease();
    public static event ObjectRelease OnObjectRelease;

    public GvrReticlePointer gvrReticlePointer;

    public bool anyTarget;
    public bool autoRestorePosition;
    public bool followGvrReticle;

    public UnityEvent onDragEvent;
    public UnityEvent onReleaseEvent;

    [SerializeField]
    private DraggerObject targetDraggerObject;

    private Transform originalParent;
    private Vector3 originalPosition;
    private Vector3 originalEularAngels;
    private Vector3 originalScale;

    private void Update()
    {
        if(followGvrReticle)
            transform.localPosition = new Vector3(0f, 0f, gvrReticlePointer.ReticleDistanceInMeters);
    }

    public void DragObject(DraggerObject draggerObject)
    {
        if(targetDraggerObject != null)
        {
            ReleaseObject();
        }

        targetDraggerObject = draggerObject;

        originalParent = targetDraggerObject.transform.parent;
        originalPosition = targetDraggerObject.transform.localPosition;
        originalEularAngels = targetDraggerObject.transform.localEulerAngles;
        originalScale = targetDraggerObject.transform.localScale;
        
        targetDraggerObject.transform.SetParent(gameObject.transform);
        targetDraggerObject.GetComponent<Collider>().enabled = false;

        if(targetDraggerObject.forceDragPosition)
            targetDraggerObject.transform.localPosition = targetDraggerObject.dragPosition;

        if (targetDraggerObject.forceDragRotation)
            targetDraggerObject.transform.localEulerAngles = targetDraggerObject.dragRotation;

        if (targetDraggerObject.forceDragScale)
            targetDraggerObject.transform.localScale = targetDraggerObject.dragScale;

        if (onDragEvent != null)
        {
            onDragEvent.Invoke();
        }

        if(OnObjectDrag != null)
        {
            OnObjectDrag();
        }
    }
    
    public void ReleaseObject(DraggerTarget draggerTarget = null)
    {
        if (targetDraggerObject == null)
            return;

        if (!targetDraggerObject.InsideTarget(anyTarget, draggerTarget))
        {
            targetDraggerObject.transform.SetParent(originalParent);

            if (autoRestorePosition)
            {
                targetDraggerObject.transform.localPosition = originalPosition;
                targetDraggerObject.transform.localEulerAngles = originalEularAngels;
                targetDraggerObject.transform.localScale = originalScale;
            }

            targetDraggerObject.GetComponent<Collider>().enabled = true;
        }

        if(targetDraggerObject.forceReleasePosition)
            targetDraggerObject.transform.localPosition = targetDraggerObject.releasePosition;

        if (targetDraggerObject.forceReleaseRotation)
            targetDraggerObject.transform.localEulerAngles = targetDraggerObject.releaseRotation;

        if (targetDraggerObject.forceReleaseScale)
            targetDraggerObject.transform.localScale = targetDraggerObject.releaseScale;

        targetDraggerObject = null;

        if(onReleaseEvent != null)
        {
            onReleaseEvent.Invoke();
        }

        if (OnObjectRelease != null)
        {
            OnObjectRelease();
        }
    }
}
