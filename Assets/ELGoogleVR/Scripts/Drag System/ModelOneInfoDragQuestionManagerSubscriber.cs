using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CardboardButtonTargetActivator))]
public class ModelOneInfoDragQuestionManagerSubscriber : MonoBehaviour
{
    private CardboardButtonTargetActivator cardboardButtonTargetActivator;

    private void Awake()
    {
        cardboardButtonTargetActivator = gameObject.GetComponent<CardboardButtonTargetActivator>();
    }

    private void OnEnable()
    {
        ModelOneInfoDragQuestionManager.OnEndSwitch += cardboardButtonTargetActivator.Activate;
        ModelOneInfoDragQuestionManager.OnBeginSwitch += cardboardButtonTargetActivator.Deactivate;
    }

    private void OnDisable()
    {
        ModelOneInfoDragQuestionManager.OnEndSwitch -= cardboardButtonTargetActivator.Activate;
        ModelOneInfoDragQuestionManager.OnBeginSwitch -= cardboardButtonTargetActivator.Deactivate;
    }
}
