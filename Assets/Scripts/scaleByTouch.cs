using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scaleByTouch : MonoBehaviour
{

    public float defaultScaleX, defaultScaleY, defaultScaleZ;
    public float currentScaleX, currentScaleY, currentScaleZ;
    public float scaleValue = 0.0f;
    public GameObject mBundleInstance = null;
    void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            //Debug.Log( mBundleInstance.transform.rotation.y + ":"+touchDeltaPosition.x );
            mBundleInstance.transform.Rotate(Vector3.up * -touchDeltaPosition.x * Time.deltaTime);
        }



        if (Input.touchCount == 2)
        {
            Touch touch0 = Input.GetTouch(0);
            Touch touch1 = Input.GetTouch(1);
            if (touch0.phase == TouchPhase.Moved && touch1.phase == TouchPhase.Moved)
            {
                Vector2 prevDist = (touch0.position - touch0.deltaPosition) - (touch1.position - touch1.deltaPosition);
                Vector2 curDist = touch0.position - touch1.position;
                float delta = curDist.magnitude - prevDist.magnitude;
                if (delta > 0)
                {
                    if (defaultScaleX >= currentScaleX * 4)
                    {

                    }
                    else if (defaultScaleX < currentScaleX - 0.1)
                    {

                    }
                    else if (defaultScaleX >= currentScaleX)
                    {

                        defaultScaleX = defaultScaleX + scaleValue;
                        defaultScaleY = defaultScaleY + scaleValue;
                        defaultScaleZ = defaultScaleZ + scaleValue;
                        mBundleInstance.transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                    else
                    {

                        defaultScaleX = defaultScaleX + scaleValue;
                        defaultScaleY = defaultScaleY + scaleValue;
                        defaultScaleZ = defaultScaleZ + scaleValue;
                        mBundleInstance.transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                }
                else if (delta < 0)
                {

                    if (defaultScaleX <= currentScaleX)
                    {

                    }
                    else if (defaultScaleX > currentScaleX)
                    {

                        defaultScaleX = defaultScaleX - scaleValue;
                        defaultScaleY = defaultScaleY - scaleValue;
                        defaultScaleZ = defaultScaleZ - scaleValue;
                        mBundleInstance.transform.localScale = new Vector3(defaultScaleX, defaultScaleY, defaultScaleZ);
                    }
                }
            }
        }


    }
    public float rotSpeed = 5;
    void OnMouseDrag()
    {
        float rotX = Input.GetAxis("Mouse X") * rotSpeed * Mathf.Deg2Rad;
        float rotY = Input.GetAxis("Mouse Y") * rotSpeed * Mathf.Deg2Rad;

        transform.RotateAround(Vector3.up, -rotX);
        transform.RotateAround(Vector3.right, rotY);
    }
}
