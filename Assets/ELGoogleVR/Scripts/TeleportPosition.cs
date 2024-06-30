using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPosition : MonoBehaviour
{
    public Vector3 Position
    {
        get { return transform.position; }
    }

    public void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
