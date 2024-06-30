using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider))]
public class OnTriggerActionsTags : MonoBehaviour
{
    public string [] triggerEnterTags;
    public string[] triggerStayTags;
    public string[] triggerExitTags;
    public UnityEvent[] triggerEnterActions;
    public UnityEvent[] triggerStayActions;
    public UnityEvent[] triggerExitActions;

    void OnTriggerEnter(Collider other)
    {
        for(int i = 0; i < triggerEnterTags.Length; i++)
        {
            if(other.tag == triggerEnterTags[i])
            {
                if (triggerEnterActions[i] != null)
                {
                    triggerEnterActions[i].Invoke();
                }
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        for (int i = 0; i < triggerStayTags.Length; i++)
        {
            if (other.tag == triggerStayTags[i])
            {
                if (triggerStayActions[i] != null)
                {
                    triggerStayActions[i].Invoke();
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        for (int i = 0; i < triggerExitTags.Length; i++)
        {
            if (other.tag == triggerExitTags[i])
            {
                if (triggerExitActions[i] != null)
                {
                    triggerExitActions[i].Invoke();
                }
            }
        }
    }
}
