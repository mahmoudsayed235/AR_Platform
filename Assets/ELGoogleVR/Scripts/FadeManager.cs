using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FadeManager : MonoBehaviour
{
    public delegate void FadeInOutBegin();
    public delegate void FadeInOutEnd();

    public static event FadeInOutBegin OnFadeInOutBegin;
    public static event FadeInOutEnd OnFadeInOutEnd;

    public Image fadeImage;
    public Color fadeColor;
    [Range(0.25f, 5f)]
    public float fadeTime = 0.25f;

    public UnityEvent onFadedInEvent;
    public UnityEvent onFadedOutEvent;
    public UnityEvent onFadeInOutEvent;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.I))
        {
            FadeIn();
        }
        if (Input.GetKeyUp(KeyCode.O))
        {
            FadeOut();
        }
        if (Input.GetKeyUp(KeyCode.F))
        {
            FadeInOut();
        }
    }

    public void FadeToBlack()
    {
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(0.0f);
        fadeImage.CrossFadeAlpha(1.0f, fadeTime, true);
    }

    public void FadeFromBlack()
    {
        fadeImage.color = Color.black;
        fadeImage.canvasRenderer.SetAlpha(1.0f);
        fadeImage.CrossFadeAlpha(0.0f, fadeTime, true);
    }

    public void FadeIn()
    {
        fadeImage.color = fadeColor;
        StartCoroutine(FadingIn());
    }

    public void FadeOut()
    {
        fadeImage.color = fadeColor;
        StartCoroutine(FadingOut());
    }

    public void FadeInOut(float duration = 0)
    {
        StartCoroutine(FadingInOut(duration));
    }

    IEnumerator FadingIn()
    {
        float fadingTime = 0.0f;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0.0f);

        while (fadingTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            fadingTime += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, fadingTime / fadeTime);
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1.0f);

        if (onFadedInEvent != null)
            onFadedInEvent.Invoke();
    }

    IEnumerator FadingOut()
    {
        float fadingTime = 0.0f;
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1.0f);

        while (fadingTime < fadeTime)
        {
            yield return new WaitForEndOfFrame();
            fadingTime += Time.deltaTime;
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1 - fadingTime / fadeTime);
        }

        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 0.0f);

        if (onFadedOutEvent != null)
            onFadedOutEvent.Invoke();
    }

    IEnumerator FadingInOut(float duration)
    {
        if (OnFadeInOutBegin != null)
            OnFadeInOutBegin();

        FadeIn();
        yield return new WaitForSeconds(fadeTime);

        if (onFadeInOutEvent != null)
        {
            onFadeInOutEvent.Invoke();
        }
        yield return new WaitForSeconds(duration);

        FadeOut();
        yield return new WaitForSeconds(fadeTime);

        if (OnFadeInOutEnd != null)
            OnFadeInOutEnd();
    }
}