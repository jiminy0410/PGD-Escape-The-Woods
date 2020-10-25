using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float m_JumpForce = 200f;                          // Amount of force added when the player jumps.
	[SerializeField] private float m_JumpForce_Crouch = 400f;                   // Amount of force added when the player jumps & crouch.
	[Range(0, 1)] [SerializeField] private float m_CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float m_MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool m_AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask m_WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform m_GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform m_CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform m_CornerCheck;                          // A position marking where to check for corners
	[SerializeField] private Collider2D m_CrouchDisableCollider;                // A collider that will be disabled when crouching

	//public PlayerMovement controller;

	float k_GroundedRadius = 0.4f; // Radius of the overlap circle to determine if grounded
	float k_CornerRadius = 0.4f; // Radius of the overlap circle to determine if grounded
	private bool m_Grounded;            // Whether or not the player is grounded.
	private bool m_Ledge_Grab;            // Whether or not the player is grounded.
	public bool press_Jump_First;
	const float k_CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	private Rigidbody2D m_Rigidbody2D;
	private bool m_FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	public float runSpeed = 20f;

	float horizontalMove = 0f;
	public bool jump = false;
	public bool jump_Short = false;
	public bool crouch = false;

	public float jump_Counter, jump_Time, hang_Counter, hang_Time;

	public bool crouch_Jump, press_Jump, double_Button_Jump;

	private void Awake()
	{
		m_Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(m_GroundCheck.position, k_GroundedRadius);
		Gizmos.DrawWireSphere(m_CeilingCheck.position, k_CeilingRadius);
		Gizmos.DrawWireSphere(m_CornerCheck.position, k_CornerRadius);
	}

	void Update()
	{
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (double_Button_Jump)
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

		if (jump_Counter < 0 || Input.GetButtonUp("Jump"))
		{
			press_Jump_First = false;
			hang_Counter = 0;
			jump_Counter = 0;
			jump = false;
		}

		if (Input.GetButtonUp("Jump") && m_Rigidbody2D.velocity.y > 0 && press_Jump)
		{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, m_Rigidbody2D.velocity.y / 4);
		}
	}

	public void Move(float move, bool crouch, bool jump, bool jump_Short)
	{
		m_Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				m_Grounded = true;
			}
		}

		if (m_Grounded)
		{
			hang_Counter = hang_Time;
		}
		else
		{
			hang_Counter -= Time.deltaTime;
		}


		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(m_CeilingCheck.position, k_CeilingRadius, m_WhatIsGround))
			{
				crouch = true;
			}
		}

		if (Physics2D.OverlapCircle(m_CornerCheck.position, k_CornerRadius, m_WhatIsGround))
		{
			m_Ledge_Grab = true;
		}
		else
		{
			m_Ledge_Grab = false;
		}

		//only control the player if grounded or airControl is turned on
		if (hang_Counter > 0 || m_AirControl)
		{
			if (m_Grounded)
			{
				// If crouching
				if (crouch)
				{
					k_GroundedRadius = 0.3f;
					// Reduce the speed by the crouchSpeed multiplier
					move *= m_CrouchSpeed;

					// Disable one of the colliders when crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = false;
				}
				else
				{
					k_GroundedRadius = 0.4f;
					// Enable the collider when not crouching
					if (m_CrouchDisableCollider != null)
						m_CrouchDisableCollider.enabled = true;
				}
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, m_Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			m_Rigidbody2D.velocity = Vector3.SmoothDamp(m_Rigidbody2D.velocity, targetVelocity, ref velocity, m_MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && m_FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}

		if (crouch_Jump)
		{
			// If the player should jump wile crouch...
			if (hang_Counter > 0 && jump)
			{
				m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
				// Add a vertical force to the player.
				m_Grounded = false;
				if (crouch && !m_Ledge_Grab)
				{
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce_Crouch));
				}
				else
				{
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				}
			}
		}
		else if (press_Jump)
		{
			// If the player should jump, short press short hop and long press long jump...
			if (hang_Counter > 0 && jump && !press_Jump_First)
			{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
				// Add a vertical force to the player.
				press_Jump_First = true;
				hang_Counter = 0;
				m_Grounded = false;
				m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce*1.3f));
				if (!m_Ledge_Grab)
				{
					jump_Counter = jump_Time;
				}
			}

			if (jump && Input.GetButton("Jump"))
			{
				if(m_Ledge_Grab)
				{
					jump_Counter = 0;
				}

				if (jump_Counter > 0)
				{
					if (jump_Counter != jump_Time)
					{
						m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce / 10));
					}
					jump_Counter -= Time.deltaTime;
				}
				else
				{
					jump_Counter = 0;
					jump = false;
				}
			}
			if (Input.GetButtonUp("Jump"))
			{
				press_Jump_First = false;
				jump_Counter = 0;
				jump = false;
			}
		}
		else if (double_Button_Jump)
		{
			// If the player should jump one button short jump other long jump...
			if (hang_Counter > 0 && (jump || jump_Short))
			{
			m_Rigidbody2D.velocity = new Vector2(m_Rigidbody2D.velocity.x, 0f);
				// Add a vertical force to the player.
				m_Grounded = false;
				if (jump && !m_Ledge_Grab)
				{
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce_Crouch));
				}
				else if (jump_Short)
				{
					m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
				}
			}
		}
	}

	void FixedUpdate()
	{
		// Move our character
		Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, jump_Short);
		if (!press_Jump)
		{
			jump = false;
		}

		jump_Short = false;
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		m_FacingRight = !m_FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}
