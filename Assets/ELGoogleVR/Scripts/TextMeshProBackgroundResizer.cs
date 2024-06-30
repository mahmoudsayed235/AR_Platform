using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

[RequireComponent(typeof(TextMeshPro))]
public class TextMeshProBackgroundResizer : MonoBehaviour
{
    public SpriteRenderer backgroundImage;
    public Vector2 multiplier;

    private TextMeshPro textMeshPro;

    private void Awake()
    {
        textMeshPro = GetComponent<TextMeshPro>();
    }

    private void Update()
    {
        Resize();
    }

    private void Resize()
    {
        if(textMeshPro.text == null || textMeshPro.text == "")
        {
            backgroundImage.transform.localScale = Vector3.zero;
            return;
        }

        backgroundImage.transform.localScale = new Vector2(textMeshPro.textBounds.size.x * multiplier.x, textMeshPro.textBounds.size.y * multiplier.y);
    }

}
