using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ButtonClickActions : MonoBehaviour
{
    public Button button;
    public UnityEvent actions;

    void OnEnable()
    {
        button.onClick.AddListener(CallActions);
    }

    void CallActions()
    {
        if(actions != null)
        {
            actions.Invoke();
        }
    }
}
