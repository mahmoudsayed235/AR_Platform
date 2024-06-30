using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventRepeater : MonoBehaviour
{
    public float repeatWait;
    public UnityEvent repeatEvent;

    private void Start()
    {
        StartCoroutine(Repeating());
    }

    IEnumerator Repeating()
    {
        while(true)
        {
            if(repeatEvent != null)
            {
                repeatEvent.Invoke();
            }

            yield return new WaitForSeconds(repeatWait);
        }
    }
}
