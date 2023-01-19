using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
	public CharacterController controller;
	
	public float speed = 13f;
	public float gravity = -40f;
	public float jumpHeight = 3f;
	
	Vector3 velocity;
	
	public Transform groundCheck;
	public float groundDistance = 0.4f;
	public LayerMask groundMask;
	bool isGrounded;
	
	public void Move(InputAction.CallbackContext context)
	{
		float x = context.ReadValue<Vector2>()[0];
		float z = context.ReadValue<Vector2>()[1];
		Vector3 move = transform.right * x + transform.forward * z;
		
		controller.Move(move * speed * Time.deltaTime);
	}

	public void Jump(InputAction.CallbackContext context)
	{
		if (!context.started)
            return;
			
		velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
		velocity.y += gravity * Time.deltaTime;
		
		controller.Move(velocity * Time.deltaTime);
	}
    void Update()
    {
		isGrounded =  Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
		
		if(isGrounded && velocity.y < 0){
			velocity.y = -6f;
		}
		
        float x = Input.GetAxis("Horizontal");
		float z = Input.GetAxis("Vertical");
		
		
    }
}