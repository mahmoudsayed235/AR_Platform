using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotateByTouch : MonoBehaviour
{
    public float rotSpeed = 20;
    public bool work = false;
    public bool vert = false;
    public bool horiz = false;
    public bool cycle360 = false;
    public void setWork(bool work)
    {
        this.work = work;
    }
    void OnMouseDrag()
    {
        if (work)
        {
            float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
            float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;


            if (vert)
                this.transform.RotateAround(Vector3.forward, -rotY);
            if (horiz)
                this.transform.RotateAround(Vector3.up, -rotX);

        }
    }
}
