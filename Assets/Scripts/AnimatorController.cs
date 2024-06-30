using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    public Animator[] animators;
    public float startPeriod;
    public float period;
    Animator currentAnimator;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("callPlayAnimation", startPeriod);

    }
    void callPlayAnimation() {
        StartCoroutine(playAnimator());
    }

       
    IEnumerator playAnimator()
    {
        for (int i = 0; i < animators.Length; i++)
        {
            animators[i].enabled=true;

        yield return new WaitForSeconds(period);

        } 
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
