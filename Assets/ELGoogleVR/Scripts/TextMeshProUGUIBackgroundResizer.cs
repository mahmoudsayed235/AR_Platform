using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent (typeof(ArabicTextMeshProUGUI))]
public class TextMeshProUGUIBackgroundResizer : MonoBehaviour
{
    public Image backgroundImage;
    public float multiplier;

    private TextMeshProUGUI textMeshProUGUI;

    private void Awake()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        backgroundImage.rectTransform.sizeDelta = new Vector2(textMeshProUGUI.textBounds.size.x * multiplier, backgroundImage.rectTransform.sizeDelta.y);
    }
}
