using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivator : MonoBehaviour
{
    public GameObject targetObject;

    public void ActivateAfter(float delay)
    {
        StartCoroutine(Activator(true, delay));
    }

    public void DeactivateAfter(float delay)
    {
        StartCoroutine(Activator(false, delay));
    }


    IEnumerator Activator(bool active, float delay)
    {
        yield return new WaitForSeconds(delay);
        targetObject.SetActive(active);
    }
}
