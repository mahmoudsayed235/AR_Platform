using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(EventTrigger))]
public class DraggerTarget : MonoBehaviour
{
    public delegate void ObjectDragged(DraggerTarget draggerTarget);
    public static event ObjectDragged OnObjectDragged;

    public int MaxObjects = 1;
    
    private List<DraggerObject> draggedObjectsInside;
    private EventTrigger trigger;

    void OnDrawGizmos()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);
        }
    }

    void OnDrawGizmosSelected()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);
        }
    }

    private void Awake()
    {
        draggedObjectsInside = new List<DraggerObject>();
        trigger = GetComponent<EventTrigger>();

        if (trigger == null)
        {
            gameObject.AddComponent<EventTrigger>();
            trigger = GetComponent<EventTrigger>();
        }

        trigger.enabled = CanDragMoreObjects() ? true : false;
    }
    
    private void Update()
    {
        trigger.enabled = CanDragMoreObjects() ? true : false;
    }

    private bool CanDragMoreObjects()
    {
        return draggedObjectsInside.Count < MaxObjects;
    }

    public bool RepositionObject(DraggerObject draggerObject)
    {
        //Debug.LogFormat("Repositioning Object: {0} inside Target: {1}", draggerObject.name, name);

        if(draggedObjectsInside.Count >=  MaxObjects)
        {
            //Debug.LogFormat("Dragger Target: {0}: Reached Max Objects", name);
            return false;
        }

        draggerObject.transform.SetParent(gameObject.transform);

        if (draggerObject.transform.GetType().Equals(typeof(RectTransform)))
        {
            draggerObject.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
        else
        {
            draggerObject.transform.localPosition = Vector3.zero;
        }

        draggerObject.transform.rotation = gameObject.transform.localRotation;

        draggedObjectsInside.Add(draggerObject);
        
        if (OnObjectDragged != null)
        {
            OnObjectDragged(this);
        }

        return true;
    }

    public int LastDraggedObjectKey()
    {
        if (draggedObjectsInside.Count == 0)
            return -1;

        return draggedObjectsInside[draggedObjectsInside.Count - 1].key;
    }

    public int ObjectsInsideCount()
    {
        return draggedObjectsInside.Count;
    }

    public bool IsObjectWithKeyInside(int key)
    {
        foreach(DraggerObject draggedObject in draggedObjectsInside)
        {
            if(draggedObject.key == key)
            {
                return true;
            }
        }

        return false;
    }
}
