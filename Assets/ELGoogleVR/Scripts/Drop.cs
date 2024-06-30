using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class Drop : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private Rigidbody _rigidbody;

    private Color color;
    public Color Color
    {
        set
        {
            color = value;
            spriteRenderer.color = color;
        }
    }

    private float speed;
    public float Speed
    {
        set
        {
            speed = value;
        }
    }

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        _rigidbody.velocity = Vector3.down * speed;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(gameObject);
    }
}
