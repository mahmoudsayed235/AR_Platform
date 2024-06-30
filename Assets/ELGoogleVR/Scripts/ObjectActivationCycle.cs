using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectActivationCycle : MonoBehaviour
{
    public GameObject[] objects;
    public bool autoHide;
    
    [Range(0.0f, 60.0f)]
    public float activateWait;

    [Range(0.0f, 60.0f)]
    public float deactivateWait;

    private bool cycling;

    public void Cycle(bool cycle)
    {
        if (cycle && cycling)
            return;

        cycling = cycle;

        if(cycling)
        {
            StartCoroutine(Cycling());
        }
    }

    IEnumerator Cycling()
    {
        while (cycling)
        {
            for (int i = 0; cycling && i < objects.Length; i++)
            {
                yield return new WaitForSeconds(activateWait);
                objects[i].SetActive(true);
            }

            for (int i = 0; cycling && i < objects.Length; i++)
            {
                yield return new WaitForSeconds(deactivateWait);
                objects[i].SetActive(false);
            }
        }

        if(autoHide)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(false);
            }
        }

    }
}
