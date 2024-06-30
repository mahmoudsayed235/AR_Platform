using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TweenAnimationManager : MonoBehaviour
{
    public Transform Target;

    public bool tweenPosition;
    public Vector3 StartPosition;
    public Vector3 EndPosition;

    public bool tweenRotation;
    public Vector3 StartRotation;
    public Vector3 EndRotation;

    public bool tweenScale;
    public Vector3 StartScale;
    public Vector3 EndScale;

    [Range(0.0f, 30.0f)]
    public float duration;

    private bool backAndForthLooping;

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.T))
        {
            TweenBackAndForth(!backAndForthLooping);
        }
    }

    public void Tween(bool forward)
    {
        StartCoroutine(Tweening(forward));
    }

    public void TweenBackAndForth(bool looping)
    {
        backAndForthLooping = looping;

        if(backAndForthLooping)
        {
            StartCoroutine(TweeningBackAndForth());
        }
    }

    public void TweenPosition(Vector3 fromPosition, Vector3 toPosition, float duration)
    {
        StartCoroutine(TweeningPosition(fromPosition, toPosition, duration));
    }

    public void TweenRotation(Vector3 fromRotation, Vector3 toRotation, float duration)
    {
        StartCoroutine(TweeningRotation(fromRotation, toRotation, duration));
    }

    public void TweenScale(Vector3 fromScale, Vector3 toScale, float duration)
    {
        StartCoroutine(TweeningScale(fromScale, toScale, duration));
    }

    IEnumerator TweeningBackAndForth()
    {
        while(backAndForthLooping)
        {
            yield return StartCoroutine(Tweening(false));
            yield return StartCoroutine(Tweening(true));
        }
    }

    IEnumerator Tweening(bool forward)
    {
        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            if(tweenPosition)
            {
                if(forward)
                {
                    Target.localPosition = Vector3.Lerp(StartPosition, EndPosition, i);
                }
                    
                else
                {
                    Target.localPosition = Vector3.Lerp(EndPosition, StartPosition, i);
                } 
            }
                
            if(tweenRotation)
            {
                if(forward)
                {
                    Target.localEulerAngles = Vector3.Lerp(StartRotation, EndRotation, i);
                }
                else
                {
                    Target.localEulerAngles = Vector3.Lerp(EndRotation, StartPosition, i);
                }
            }
                
            if(tweenScale)
            {
                if (forward)
                {
                    Target.localScale = Vector3.Lerp(StartScale, EndScale, i);
                }
                else
                {
                    Target.localScale = Vector3.Lerp(EndScale, StartScale, i);
                }
            }
            
            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TweeningPosition(Vector3 fromPosition, Vector3 toPosition, float duration)
    {
        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            Target.localPosition = Vector3.Lerp(fromPosition, toPosition, i);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TweeningRotation(Vector3 fromRotation, Vector3 toRotation, float duration)
    {
        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            Target.localEulerAngles = Vector3.Lerp(fromRotation, toRotation, i);

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator TweeningScale(Vector3 fromScale, Vector3 toScale, float duration)
    {
        float i = 0.0f;
        float rate = 1.0f / duration;

        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;

            Target.localScale = Vector3.Lerp(fromScale, toScale, i);

            yield return new WaitForEndOfFrame();
        }
    }
}
