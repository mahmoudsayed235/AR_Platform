using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Text;
using System;

[RequireComponent(typeof(TextMeshPro))]
public class ArabicTextMeshPro : MonoBehaviour
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

			if (textMeshPro != null)
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
    
	private TextMeshPro textMeshPro;
	public TextMeshPro TextMeshPro
	{
		set
		{
			if (value != null)
				textMeshPro = value;

			if (textMeshPro != null)
				UpdateText();
		}

		get
		{
			return textMeshPro;
		}
	}

    

    void Awake()
	{
		textMeshPro = GetComponent<TextMeshPro>();
    }

	public void UpdateText()
	{
        textMeshPro.text = fix ? ArabicSupport.ArabicFixer.Fix(text, tashkeel, arabicNumbers) : text;
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
        textMeshPro.enabled = false;
    }

    private void OnFadeInOutEnd()
    {
        textMeshPro.enabled = true;
    }
}
