using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectFlasher : MonoBehaviour
{
    public Transform targetObject;
    public float defaultDuration;
    public float flashInAfter;
    public float flashOutAfter;
    public float hideObjectAfter;

    public AudioSource flashSfxAudioSource;

    private float wait = 0.1f;
    private Coroutine flashingInCoroutine;
    private Coroutine flashingOutCoroutine;

    public void FlashIn(float duration = 0)
    {
        if (flashingInCoroutine != null)
            StopCoroutine(flashingInCoroutine);

        if (flashingOutCoroutine != null)
            StopCoroutine(flashingOutCoroutine);

        targetObject.localScale = Vector3.zero;
        targetObject.gameObject.SetActive(true);
        flashingInCoroutine = StartCoroutine(FlashingIn(duration > 0 ? duration : defaultDuration));

        if(flashSfxAudioSource != null && flashSfxAudioSource.clip != null)
        {
            flashSfxAudioSource.Play();
        }
    }
    
    public void FlashOut(float duration = 0)
    {
        if (flashingInCoroutine != null)
            StopCoroutine(flashingInCoroutine);

        if (flashingOutCoroutine != null)
            StopCoroutine(flashingOutCoroutine);

        targetObject.gameObject.SetActive(true);

        flashingOutCoroutine = StartCoroutine(FlashingOut(duration > 0 ? duration : defaultDuration));
    }

    // receive 4 params as vector4 x: duration | y: flashOutAfter | z: hideObjectAfter | w: targetScale
    public void FlashIn(Vector4 flashParams)
    {
        targetObject.gameObject.SetActive(true);

        if (flashingInCoroutine != null)
            StopCoroutine(flashingInCoroutine);

        if (flashingOutCoroutine != null)
            StopCoroutine(flashingOutCoroutine);

        flashingInCoroutine = StartCoroutine(FlashingIn(flashParams.x > 0 ? flashParams.x : defaultDuration, flashParams.y, flashParams.z, flashParams.w));
    }

    // receive 4 params as vector4 x: duration | y: flashInAfter | z: hideObjectAfter | w: targetScale
    public void FlashOut(Vector4 flashParams)
    {
        targetObject.gameObject.SetActive(true);

        if (flashingInCoroutine != null)
            StopCoroutine(flashingInCoroutine);

        if (flashingOutCoroutine != null)
            StopCoroutine(flashingOutCoroutine);

        targetObject.localScale = Vector3.one;
        flashingOutCoroutine = StartCoroutine(FlashingOut(flashParams.x > 0 ? flashParams.x : defaultDuration, flashParams.y, flashParams.z, flashParams.w));
    }

    IEnumerator FlashingIn(float duration)
    {
        int steps = Mathf.FloorToInt(duration / wait);
        float step = 1.0f / steps;

        for(int i = 0; i < steps; i++)
        {
            targetObject.localScale = Vector3.one * step * i;
            yield return wait;
        }

        targetObject.localScale = Vector3.one;

        if(flashOutAfter > 0)
        {
            yield return new WaitForSeconds(flashOutAfter);

            FlashOut();
        }

        if (hideObjectAfter > 0)
        {
            yield return new WaitForSeconds(hideObjectAfter);

            targetObject.gameObject.SetActive(false);
        }
    }

    IEnumerator FlashingOut(float duration)
    {
        int steps = Mathf.FloorToInt(duration / wait);
        float step = 1.0f / steps;

        for (int i = steps; i >= 0 ; i--)
        {
            targetObject.localScale = Vector3.one * step * i;
            yield return wait;
        }

        targetObject.localScale = Vector3.zero;

        if (flashInAfter > 0)
        {
            yield return new WaitForSeconds(flashInAfter);

            FlashIn();
        }

        if (hideObjectAfter > 0)
        {
            yield return new WaitForSeconds(hideObjectAfter);

            targetObject.gameObject.SetActive(false);
        }
    }

    IEnumerator FlashingIn(float duration, float flashOutAfter, float hideObjectAfter, float targetScale)
    {
        int steps = Mathf.FloorToInt(duration / wait);
        float step = Mathf.Abs(targetObject.localScale.x - targetScale) / steps;

        for (int i = 1; i <= steps; i++)
        {
            targetObject.localScale = Vector3.one * step * i;
            yield return wait;
        }

        targetObject.localScale = new Vector3(targetScale, targetScale, targetScale);

        if (flashOutAfter > 0)
        {
            yield return new WaitForSeconds(flashOutAfter);

            FlashOut();
        }

        if(hideObjectAfter > 0)
        {
            yield return new WaitForSeconds(hideObjectAfter);

            targetObject.gameObject.SetActive(false);
        }
    }

    IEnumerator FlashingOut(float duration, float flashOutAfter, float hideObjectAfter, float targetScale)
    {
        int steps = Mathf.FloorToInt(duration / wait);
        float step = Mathf.Abs(targetObject.localScale.x - targetScale) / steps;

        for (int i = steps; i > 0; i--)
        {
            targetObject.localScale = Vector3.one * step * i;
            yield return wait;
        }

        targetObject.localScale = new Vector3(targetScale, targetScale, targetScale);

        if (flashInAfter > 0)
        {
            yield return new WaitForSeconds(flashInAfter);

            FlashIn();
        }

        if (hideObjectAfter > 0)
        {
            yield return new WaitForSeconds(hideObjectAfter);

            targetObject.gameObject.SetActive(false);
        }
    }
}
