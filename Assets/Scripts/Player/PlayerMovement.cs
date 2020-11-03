using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

	public CharacterController2D controller;
	private Rigidbody2D m_Rigidbody2D;

	public float runSpeed = 40f;

	float horizontalMove = 0f;
	public bool jump = false;
	public bool jump_Short = false;
	public bool crouch = false;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
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

		if (Input.GetButtonUp("Jump") && m_Rigidbody2D.velocity.y > 0 && controller.press_Jump)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y / 4);
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
