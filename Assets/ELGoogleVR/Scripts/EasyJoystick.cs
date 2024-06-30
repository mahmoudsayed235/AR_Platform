using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

[RequireComponent(typeof(RectTransform))]
public class EasyJoystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public RectTransform stick;                     //stick image;
    public float returnRate = 15.0F;                //default position returning speed;
    public float dragRadius = 65.0f;                //drag radius;
    public AlphaControll colorAlpha;
    public GvrPointerInputModule gvrPointerInputModule; // used to refresh gvr pointer input module

    public event Action<EasyJoystick, Vector2> OnStartJoystickMovement;
    public event Action<EasyJoystick, Vector2> OnJoystickMovement;
    public event Action<EasyJoystick> OnEndJoystickMovement;

    public UnityEvent OnStartJoystickMovementEvent;
    public UnityEvent OnJoystickMovementEvent;
    public UnityEvent OnEndJoystickMovementEvent;

    private bool _returnHandle, pressed, isEnabled = true;
    private RectTransform _canvas;
    private Vector3 globalStickPos;
    private Vector2 stickOffset;
    private CanvasGroup canvasGroup;

    public Vector2 Coordinates
    {
        get
        {
            if (stick.anchoredPosition.magnitude < dragRadius)
                return stick.anchoredPosition / dragRadius;
            return stick.anchoredPosition.normalized;
        }
    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        pressed = true;
        _returnHandle = false;
        stickOffset = GetJoystickOffset(eventData);
        stick.anchoredPosition = stickOffset;
        if (OnStartJoystickMovement != null)
            OnStartJoystickMovement(this, Coordinates);

        if (OnStartJoystickMovementEvent != null)
            OnStartJoystickMovementEvent.Invoke();
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        stickOffset = GetJoystickOffset(eventData);
        stick.anchoredPosition = stickOffset;
        if (OnJoystickMovement != null)
            OnJoystickMovement(this, Coordinates);

        if (OnJoystickMovementEvent != null)
            OnJoystickMovementEvent.Invoke();
    }

    void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
    {
        pressed = false;
        _returnHandle = true;
        if (OnEndJoystickMovement != null)
            OnEndJoystickMovement(this);

        if (OnEndJoystickMovementEvent != null)
            OnEndJoystickMovementEvent.Invoke();
    }

    private Vector2 GetJoystickOffset(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(_canvas, eventData.position, eventData.pressEventCamera, out globalStickPos))
            stick.position = globalStickPos;
        var handleOffset = stick.anchoredPosition;
        if (handleOffset.magnitude > dragRadius)
        {
            handleOffset = handleOffset.normalized * dragRadius;
            stick.anchoredPosition = handleOffset;
        }
        return handleOffset;
    }

    private void Start()
    {
        canvasGroup = GetComponent("CanvasGroup") as CanvasGroup;
        _returnHandle = true;
        var touchZone = GetComponent("RectTransform") as RectTransform;
        touchZone.pivot = Vector2.one * 0.5F;
        stick.transform.SetParent(transform);
        var curTransform = transform;
        do
        {
            if (curTransform.GetComponent<Canvas>() != null)
            {
                _canvas = curTransform.GetComponent("RectTransform") as RectTransform; ;
                break;
            }
            curTransform = transform.parent;
        }
        while (curTransform != null);
    }

    private void Update()
    {
        if (_returnHandle)
            if (stick.anchoredPosition.magnitude > Mathf.Epsilon)
                stick.anchoredPosition -= new Vector2(stick.anchoredPosition.x * returnRate,
                                                      stick.anchoredPosition.y * returnRate) * Time.deltaTime;
            else
                _returnHandle = false;

        switch (isEnabled)
        {
            case true:
                canvasGroup.alpha = pressed ? colorAlpha.pressedAlpha : colorAlpha.idleAlpha;
                canvasGroup.interactable = canvasGroup.blocksRaycasts = true;
                break;
            case false:
                canvasGroup.alpha = 0;
                canvasGroup.interactable = canvasGroup.blocksRaycasts = false;
                break;
        }

        gvrPointerInputModule.Process();
    }

    public bool IsPressed()
    {
        return pressed;
    }

    public void Enable(bool enable)
    {
        isEnabled = enable;
    }

    public void Stop()
    {
        pressed = false;
        _returnHandle = true;
    }
}


[Serializable]
public class AlphaControll
{
    public float idleAlpha = 0.5F, pressedAlpha = 1.0F;
}















