using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARControlSystem : MonoBehaviour
{
    public GameObject[] childs;

    public void choose(int index)
    {
        if (index == -1) {
            for (int i = 0; i < childs.Length; i++)
            {
                childs[i].SetActive(true);
            }
        }
        else
        {
            for (int i = 0; i < childs.Length; i++)
            {
                if (i == index)
                {
                    childs[i].SetActive(true);
                }
                else
                {
                    childs[i].SetActive(false);
                }
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
