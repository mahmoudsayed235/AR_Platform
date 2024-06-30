using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EnjoyLearning.VR.SDK;

[RequireComponent(typeof(CardboardButtonTarget))]
public class CardboardButtonTargetActivator : MonoBehaviour
{
    public bool activeOnEnable;

    private CardboardButtonTarget mCardboardButtonTarget;
    private Collider mCollider;
    private Button mButton;

    private bool active;

    private void Awake()
    {
        mCollider = GetComponent<Collider>();
        mButton = GetComponent<Button>();
        mCardboardButtonTarget = GetComponent<CardboardButtonTarget>();
    }

    private void OnEnable()
    {
        if(activeOnEnable)
        {
            Activate();
        }
        else
        {
            Deactivate();
        }
    }

    public void Activate()
    {
        active = true;

        if(mCollider != null)
            mCollider.enabled = true;

        if(mButton != null)
        {
            mButton.interactable = true;
            mButton.image.raycastTarget = true;
        }

        mCardboardButtonTarget.enabled = true;
    }

    public void Deactivate()
    {
        active = false;

        if (mCollider != null)
            mCollider.enabled = false;

        if (mButton != null)
        {
            mButton.interactable = false;
            mButton.image.raycastTarget = false;
        }

        mCardboardButtonTarget.enabled = false;
    }

    public bool IsActive()
    {
        return active;
    }
}
