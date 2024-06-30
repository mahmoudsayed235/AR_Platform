using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class videoController : MonoBehaviour
{
    public RenderHeads.Media.AVProVideo.MediaPlayer mp;
    public DefaultTrackableEventHandler dTracker;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    public GameObject screen;
    // Update is called once per frame
    void Update()
    {
        if (screen.GetComponent<MeshRenderer>().enabled) { 
                print("playing");
                mp.Play();
            
    }
        else
        {
           
                print("playing");
    mp.Pause();
            
        }
    }
}
