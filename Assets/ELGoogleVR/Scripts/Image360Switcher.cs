using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Image360Switcher : MonoBehaviour
{
    public ObjectFlasher image360Flasher_1;
    public ObjectFlasher image360Flasher_2;
    public MeshRenderer image360Render_1;
    public MeshRenderer image360Render_2;

    public Vector4 flashInParams;
    public Vector4 flashOutParams;

    public bool cycling;
    public float cycleWait;
    public Material[] image360;

    private int current360ImageRenderer;
    private MeshRenderer flashedIn360Image;
    private Coroutine cyclingCoroutine;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.S))
        {
            Switch();
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            Cycle(true);
        }

        if (Input.GetKeyUp(KeyCode.X))
        {
            Cycle(false);
        }
    }

    private void OnEnable()
    {
        current360ImageRenderer = -1;
        flashedIn360Image = image360Render_1;
        Switch();
    }

    public void Switch()
    {
        current360ImageRenderer++;

        if(current360ImageRenderer >= image360.Length)
        {
            if (cycling)
            {
                current360ImageRenderer = -1;
                //Debug.LogFormat("360 Image Switcher: Cycling");
                Switch();
                return;
            }

            //Debug.LogFormat("360 Image Switcher: No more 360 image");
            return;
        }

        Switch(image360[current360ImageRenderer]);
    }

    public void Switch(Material material)
    {
        if (flashedIn360Image == image360Render_1)
        {
            //Debug.LogFormat("360 Image Switcher: Switching to image -> {0} at Rendreer 2", image360[current360ImageRenderer].name);
            flashedIn360Image = image360Render_2;
            image360Render_2.material = material;
            image360Flasher_2.FlashIn(flashInParams);
            image360Flasher_1.FlashOut(flashOutParams);

        }
        else
        {
            //Debug.LogFormat("360 Image Switcher: Switching to image -> {0} at Rendreer 1", image360[current360ImageRenderer].name);
            flashedIn360Image = image360Render_1;
            image360Render_1.material = material;
            image360Flasher_1.FlashIn(flashInParams);
            image360Flasher_2.FlashOut(flashOutParams);
        }
    }

    public void Cycle(bool cycle)
    {
        cycling = cycle;
        //Debug.LogFormat("360 Image Switcher: Cycling -> {0}", cycling);

        if (cycling)
        {
            if(cyclingCoroutine == null)
            {
                cyclingCoroutine = StartCoroutine(Cycling());
            }
        }
        else
        {
            if(cyclingCoroutine != null)
            {
                StopCoroutine(cyclingCoroutine);
                cyclingCoroutine = null;
            }
        }
    }

    private IEnumerator Cycling()
    {
        while(true)
        {
            yield return new WaitForSeconds(cycleWait);
            Switch();
        }
    }
}
