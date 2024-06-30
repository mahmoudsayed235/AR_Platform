using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playAnimationController : MonoBehaviour
{

    public void PlayWithTrigger(string triggerName)
    {
        this.gameObject.GetComponent<Animator>().SetTrigger(triggerName);
    }
}
