using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardboardButtonTargetActivator))]
public class ObjectDraggerSubscriber : MonoBehaviour
{
    private CardboardButtonTargetActivator cardboardButtonTargetActivator;

    private void Awake()
    {
        cardboardButtonTargetActivator = gameObject.GetComponent<CardboardButtonTargetActivator>();
    }

    private void OnEnable()
    {
        ObjectDragger.OnObjectDrag += cardboardButtonTargetActivator.Activate;
        ObjectDragger.OnObjectRelease += cardboardButtonTargetActivator.Deactivate;
    }

    private void OnDisable()
    {
        ObjectDragger.OnObjectDrag -= cardboardButtonTargetActivator.Activate;
        ObjectDragger.OnObjectRelease -= cardboardButtonTargetActivator.Deactivate;
    }
}
