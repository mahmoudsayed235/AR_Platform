using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenOrientation : MonoBehaviour
{
    public ScreenOrientation orientation;
    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = orientation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
