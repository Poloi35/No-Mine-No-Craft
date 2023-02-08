using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseLook : MonoBehaviour
{
	public float mouseSensitivity = 15f;
	public Transform playerTransform;
	private float xRotation = 0f;

	private void Awake() {
		Singleton.instance.playerInputActions.Player.Look.performed += Look;
	}

	public void Look(InputAction.CallbackContext context)
	{
		Vector2 mouse = context.ReadValue<Vector2>() * mouseSensitivity * Time.deltaTime;
		xRotation -= mouse.y;
		xRotation = Mathf.Clamp(xRotation,-90f,90f);
		
		transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
		// Debug.Log(transform.localRotation);
		// Debug.Log(xRotation);
		playerTransform.Rotate(Vector3.up * mouse.x);
	}
}
