using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class TextQueueManager : MonoBehaviour
{
    public TextMeshPro[] textMeshProTexts;
    public Text[] uGuiTexts;

    private int lastUpdateText = -1;

    private void OnEnable()
    {
        TimerManager.OnTimerOff += UpdateNextText;
    }

    private void OnDisable()
    {
        TimerManager.OnTimerOff -= UpdateNextText;
    }

    public void UpdateText(Text uGuiText)
    {
        UpdateNextText(uGuiText.text);
    }

    public void UpdateText(TextMeshPro textMeshProText)
    {
        UpdateNextText(textMeshProText.text);
    }

    public void UpdateText(string text)
    {
        UpdateNextText(text);
    }

    private void UpdateNextText(string text)
    {
        lastUpdateText++;

        if (textMeshProTexts != null && textMeshProTexts.Length > lastUpdateText)
        {
            textMeshProTexts[lastUpdateText].text = text;
        }

        if (uGuiTexts != null && uGuiTexts.Length > lastUpdateText)
        {
            uGuiTexts[lastUpdateText].text = text;
        }
    }
}
