using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent (typeof(Camera))]
public class TouchCamera : MonoBehaviour
{
    public EasyJoystick joystick;

    [Range(0.0f, 10.0f)]
    public float smooth;

    private const string AXIS_MOUSE_X = "Mouse X";
	private const string AXIS_MOUSE_Y = "Mouse Y";

	private static readonly Vector3 NECK_OFFSET = new Vector3(0, 0.075f, 0.08f);

	private float mouseX = 0;
	private float mouseY = 0;
	private float mouseZ = 0;

	private bool activeMouse;

	public Vector3 HeadPosition { get; private set; }
	public Quaternion HeadRotation { get; private set; }
    
    void Update()
	{
        mouseX += joystick.Coordinates.x * smooth;
        if (mouseX <= -180)
        {
            mouseX += 360;
        }
        else if (mouseX > 180)
        {
            mouseX -= 360;
        }

        mouseY -= joystick.Coordinates.y * smooth;
        mouseY = Mathf.Clamp(mouseY, -85, 85);

        HeadRotation = Quaternion.Euler(mouseY, mouseX, mouseZ);
        HeadPosition = HeadRotation * NECK_OFFSET - NECK_OFFSET.y * Vector3.up;

        transform.localPosition = HeadPosition * transform.lossyScale.y;
        transform.localRotation = HeadRotation;
    }

    public void Recenter()
	{
		mouseX = mouseZ = 0;

		HeadRotation = Quaternion.Euler(mouseY, mouseX, mouseZ);
		HeadPosition = HeadRotation * NECK_OFFSET - NECK_OFFSET.y * Vector3.up;

		transform.localPosition = HeadPosition * transform.lossyScale.y;
		transform.localRotation = HeadRotation;
	}
}
