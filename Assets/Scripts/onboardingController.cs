using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class onboardingController : MonoBehaviour
{
    public GameObject[] slides;
    public Image[] sliderIcons;
    public Sprite selected;
    public Sprite unselected;
    int index = 0;
    public Scrollbar sb;
    float perviousValue;
    int currentImage=1;

    void Update()
    {
        
     
         perviousValue = sb.value;
         if (Input.touchCount == 0)
         {

            if (sb.value >= 0f && sb.value <= 0.1f)
            {
                sliderIcons[0].sprite = selected;
                sliderIcons[1].sprite = unselected;
                sliderIcons[2].sprite = unselected;
                sliderIcons[3].sprite = unselected;
                sliderIcons[4].sprite = unselected;
            }
            else if(sb.value > 0.1f && sb.value <= 0.3f)
            {
                sliderIcons[0].sprite = unselected;
                sliderIcons[1].sprite = selected;
                sliderIcons[2].sprite = unselected;
                sliderIcons[3].sprite = unselected;
                sliderIcons[4].sprite = unselected;
            }
            else if (sb.value > 0.3f && sb.value <=.5f)
            {
                sliderIcons[0].sprite = unselected;
                sliderIcons[1].sprite = unselected;
                sliderIcons[2].sprite = selected;
                sliderIcons[3].sprite = unselected;
                sliderIcons[4].sprite = unselected;
            } else if(sb.value > 0.5f && sb.value <= 0.78f)
            {
                sliderIcons[0].sprite = unselected;
                sliderIcons[1].sprite = unselected;
                sliderIcons[2].sprite = unselected;
                sliderIcons[3].sprite = selected;
                sliderIcons[4].sprite = unselected;
            }
            else if (sb.value > 0.78f && sb.value <=1f)
            {
                sliderIcons[0].sprite = unselected;
                sliderIcons[1].sprite = unselected;
                sliderIcons[2].sprite = unselected;
                sliderIcons[3].sprite = unselected;
                sliderIcons[4].sprite = selected;
            }




        }     

    }

    public void select(int index)
    {
        print(sb.value);
        print(index);
        if (index == 1)
        {
            sliderIcons[0].sprite = selected;
            sliderIcons[1].sprite = unselected;
            sliderIcons[2].sprite = unselected;
            sliderIcons[1].sprite = unselected;
            sliderIcons[2].sprite = unselected;
            sb.value = 0.0f;
        }
        else if(index == 2)
        {
            sliderIcons[1].sprite = selected;
            sliderIcons[0].sprite = unselected;
            sliderIcons[2].sprite = unselected;
            sliderIcons[3].sprite = unselected;
            sliderIcons[4].sprite = unselected;
            sb.value = 0.3f;
        } else if (index == 3)
        {
            sliderIcons[2].sprite = selected;
            sliderIcons[1].sprite = unselected;
            sliderIcons[0].sprite = unselected;
            sliderIcons[3].sprite = unselected;
            sliderIcons[4].sprite = unselected;
            sb.value = .5f;
        } else if (index == 4)
        {
            sliderIcons[3].sprite = selected;
            sliderIcons[1].sprite = unselected;
            sliderIcons[0].sprite = unselected;
            sliderIcons[2].sprite = unselected;
            sliderIcons[4].sprite = unselected;
            sb.value = .7f;
        } else if (index == 5)
        {
            sliderIcons[4].sprite = selected;
            sliderIcons[1].sprite = unselected;
            sliderIcons[0].sprite = unselected;
            sliderIcons[3].sprite = unselected;
            sliderIcons[2].sprite = unselected;
            sb.value = 1f;
        }

    }
}
