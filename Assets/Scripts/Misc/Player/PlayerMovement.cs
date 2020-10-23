using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	public bool jump = false;
	public bool jump_Short = false;
	public bool crouch = false;

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (controller.double_Button_Jump)
		{
			if (Input.GetButtonDown("Jump_Short"))
			{
				jump_Short = true;
			}
		}


		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		if (controller.jump_Counter < 0 || Input.GetButtonUp("Jump"))
		{
			controller.press_Jump_First = false;
			controller.hang_Counter = 0;
			controller.jump_Counter = 0;
			jump = false;
		}
	}

	void FixedUpdate()
	{
		// Move our character
		controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, jump_Short);
		if (!controller.press_Jump)
		{
			jump = false;
		}

		jump_Short = false;
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{

	}
}
