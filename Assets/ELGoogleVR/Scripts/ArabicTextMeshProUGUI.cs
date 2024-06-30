using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ArabicTextMeshProUGUI : MonoBehaviour
{
	public bool fix = true;
	
    [SerializeField]
    private string text;
    public string Text
    {
        set
        {
            if (value != null)
                text = value;

            if (textMeshProUGUI != null)
                UpdateText();
        }

        get
        {
            return text;
        }
    }

    [SerializeField]
    public bool tashkeel;
    public bool Tashkeel
    {
        set
        {
            tashkeel = value;
            UpdateText();
        }

        get
        {
            return tashkeel;
        }
    }

    [SerializeField]
    public bool arabicNumbers;
    public bool ArabicNumbers
    {
        set
        {
            arabicNumbers = value;
            UpdateText();
        }

        get
        {
            return arabicNumbers;
        }
    }

    private TextMeshProUGUI textMeshProUGUI;
    public TextMeshProUGUI TextMeshProUGUI
    {
        set
        {
            if (value != null)
                textMeshProUGUI = value;

            if (textMeshProUGUI != null)
                UpdateText();
        }

        get
        {
            return textMeshProUGUI;
        }
    }

    void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        textMeshProUGUI.SetAllDirty();
    }

    public void UpdateText()
    {
		textMeshProUGUI.text = fix ? ArabicSupport.ArabicFixer.Fix(text, tashkeel, arabicNumbers) : text;
    }

    private void OnEnable()
    {
        FadeManager.OnFadeInOutBegin += OnFadeInOutBegin;
        FadeManager.OnFadeInOutEnd += OnFadeInOutEnd;
    }

    private void OnDisable()
    {
        FadeManager.OnFadeInOutBegin -= OnFadeInOutBegin;
        FadeManager.OnFadeInOutEnd -= OnFadeInOutEnd;
    }

    private void OnFadeInOutBegin()
    {
        textMeshProUGUI.enabled = false;
    }

    private void OnFadeInOutEnd()
    {
        textMeshProUGUI.enabled = true;
    }
}
