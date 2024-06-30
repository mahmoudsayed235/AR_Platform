using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformReseter : MonoBehaviour
{
    public bool resetParent;
    public Transform parent;

    public bool resetPosition;
    public Vector3 position;

    public bool resetRotation;
    public Vector3 rotation;

    public bool resetScale;
    public Vector3 scale;

    public void ResetTransform()
    {
        if (resetParent)
            transform.parent = parent;

        if (resetPosition)
            transform.localPosition = position;

        if (resetRotation)
            transform.localEulerAngles = rotation;

        if (resetScale)
            transform.localScale = scale;
    }
}
