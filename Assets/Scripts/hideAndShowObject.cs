using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class hideAndShowObject : MonoBehaviour
{
    public float startHide = 0;
    public GameObject[] hide;
    public Image[] image;
    public float startShow = 0;
    public GameObject[] show;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Hide", startHide);
        Invoke("Show", startShow);
    }
    void Hide()
    {
        for (int i = 0; i < hide.Length; i++)
        {
            hide[i].SetActive(false);
        }
    }
    void Show()
    {
        for (int i = 0; i < show.Length; i++)
        {
            show[i].SetActive(true);
        }
        for (int i = 0; i < image.Length; i++)
        {
            image[i].enabled=true;
        }
    }


}
