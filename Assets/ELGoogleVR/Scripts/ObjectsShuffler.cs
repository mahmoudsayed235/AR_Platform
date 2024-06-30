using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsShuffler : MonoBehaviour
{
    public GameObject[] objects;

    public void Shuffle()
    {
        List<int> shuffleIndices = new List<int>(objects.Length);

        for(int i = 0; i < objects.Length; i++)
        {
            shuffleIndices.Add(i);
        }

        shuffleIndices.Shuffle();

        for (int i = 0; i < objects.Length; i++)
        {
            SwapTarnsformParents(objects[i].transform, objects[shuffleIndices[i]].transform);
        }
    }

    private void SwapTarnsformParents(Transform t1, Transform t2)
    {
        Transform temp = t1.parent;
        t1.parent = t2.parent;
        t2.parent = temp;

        ResetTransform(t1);
        ResetTransform(t2);
    }

    private void ResetTransform(Transform t)
    {
        t.localPosition = Vector3.zero;
        t.localPosition = Vector3.zero;
        t.localEulerAngles = Vector3.zero;
    }
}
