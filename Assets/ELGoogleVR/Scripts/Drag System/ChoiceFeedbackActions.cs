using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChoiceFeedbackActions : MonoBehaviour
{
    public UnityEvent correctFeedbackActions;
    public UnityEvent wrongFeedbackActions;

    public void Feedback(bool correct)
    {
        if(correct)
        {
            correctFeedbackActions.Invoke();
        }
        else
        {
            wrongFeedbackActions.Invoke();
        }
    }
}
