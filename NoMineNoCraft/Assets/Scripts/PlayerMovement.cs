using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	private Rigidbody rb;
	private PlayerInput playerInput;

	public float maxSpeed = 7f;
	public float airSpeedMultiplier = .4f;
	public float jumpForce = 5f;
	private float jumpCooldown = .2f;
	private bool isJumping = false;
	public bool isReadyToJump = true;
	Vector2 input;

	public BoxCollider bc;
	private bool isGrounded;
	public float groundDrag = 5f;
	public LayerMask ground;

	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (context.performed)
			isJumping = true;
		else if (context.canceled)
			isJumping = false;
	}

	private void ResetJump()
	{
		isReadyToJump = true;
	}

	public void Move(InputAction.CallbackContext context)
	{
		input = context.ReadValue<Vector2>();
	}

	private void SpeedControl()
	{
		Vector2 flatVel = new Vector2(rb.velocity.x, rb.velocity.z);

		if (flatVel.magnitude > maxSpeed)
		{
			flatVel = flatVel.normalized * maxSpeed;
			rb.velocity = new Vector3(flatVel.x, rb.velocity.y, flatVel.y);
		}
	}

	private void FixedUpdate()
	{
		Vector3 force = 10f * maxSpeed * (transform.right * input.x + transform.forward * input.y);
		if (isGrounded)
			rb.AddForce(force, ForceMode.Force);
		else
			rb.AddForce(force * airSpeedMultiplier, ForceMode.Force);
	}

	private void Update()
	{
		// Ground check
		isGrounded = Physics.BoxCast(transform.position, new Vector3(bc.size.x * .5f * .999f, .01f, bc.size.z * .5f * .999f), Vector3.down, Quaternion.identity, bc.size.y * .5f, ground);

		if (isReadyToJump && isGrounded && isJumping)
		{
			isReadyToJump = false;
			rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
			rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

			Invoke(nameof(ResetJump), jumpCooldown);
		}

		// Handle drag
		if (isGrounded)
			rb.drag = groundDrag;
		else
			rb.drag = 0;

		// Clamp speed
		SpeedControl();
	}
	
}