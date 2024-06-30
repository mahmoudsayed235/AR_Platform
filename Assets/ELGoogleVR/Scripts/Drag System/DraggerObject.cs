using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DraggerObject : MonoBehaviour
{
    public DraggerTarget target;
    public int key;

    public bool forceDragPosition;
    public Vector3 dragPosition;

    public bool forceReleasePosition;
    public Vector3 releasePosition;

    public bool forceDragRotation;
    public Vector3 dragRotation;

    public bool forceReleaseRotation;
    public Vector3 releaseRotation;

    public bool forceDragScale;
    public Vector3 dragScale;

    public bool forceReleaseScale;
    public Vector3 releaseScale;


    void OnDrawGizmos()
    {
        Collider collider = GetComponent<Collider>();
        if(collider != null)
        {
            Gizmos.color = Color.red;
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

    public bool InsideTarget(bool anyTarget = false, DraggerTarget draggerTarget = null)
    {
        if (draggerTarget != null)
        {
            if (anyTarget)
            {
                //Debug.LogFormat("Dragger Object: {0} is Inside Target: {1}", gameObject.name, col.name);
                return draggerTarget.RepositionObject(this);
            }

            if (draggerTarget == target)
            {
                //Debug.LogFormat("Dragger Object: {0} is Inside Target: {1}", gameObject.name, col.name);
                return draggerTarget.RepositionObject(this);
            }
        }

        Collider[] collidedObjects = Physics.OverlapBox(GetComponent<Collider>().bounds.center, GetComponent<Collider>().bounds.size);

        foreach (Collider col in collidedObjects)
        {
            //Debug.LogFormat("{0} colliding with {1}", gameObject.name, col.name);
            draggerTarget = col.GetComponent<DraggerTarget>();

            if(draggerTarget != null)
            {
                if (anyTarget)
                {
                    //Debug.LogFormat("Dragger Object: {0} is Inside Target: {1}", gameObject.name, col.name);
                    return draggerTarget.RepositionObject(this);
                }

                if (draggerTarget == target)
                {
                    //Debug.LogFormat("Dragger Object: {0} is Inside Target: {1}", gameObject.name, col.name);
                    return draggerTarget.RepositionObject(this);
                }
            }
        }

        return false;
    }
}
