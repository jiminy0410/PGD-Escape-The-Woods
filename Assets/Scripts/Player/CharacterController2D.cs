using System.Collections;
using UnityEngine;

public class CharacterController2D : MonoBehaviour
{
	[SerializeField] private float JumpForce = 200f;                          // Amount of force added when the player jumps.
	[Range(0, 1)] [SerializeField] private float CrouchSpeed = .36f;          // Amount of maxSpeed applied to crouching movement. 1 = 100%
	[Range(0, .3f)] [SerializeField] private float MovementSmoothing = .05f;  // How much to smooth out the movement
	[SerializeField] private bool AirControl = false;                         // Whether or not a player can steer while jumping;
	[SerializeField] private LayerMask WhatIsGround;                          // A mask determining what is ground to the character
	[SerializeField] private Transform GroundCheck;                           // A position marking where to check if the player is grounded.
	[SerializeField] private Transform CeilingCheck;                          // A position marking where to check for ceilings
	[SerializeField] private Transform CornerCheck;                          // A position marking where to check for corners
	[SerializeField] private Collider2D CrouchDisableCollider;                // A collider that will be disabled when crouching

	//public PlayerMovement controller;

	float GroundedRadius = 0.3f; // Radius of the overlap circle to determine if grounded
	float CornerRadius = 0.4f; // Radius of the overlap circle to determine if grounded
	public bool Grounded;            // Whether or not the player is grounded.
	[SerializeField] private bool Ledge_Grab, Wall_Slide;            // Whether or not the player is grounded.
	const float CeilingRadius = .2f; // Radius of the overlap circle to determine if the player can stand up
	public Transform sprite;
	private Rigidbody2D Rigidbody2D;
	public PhysicsMaterial2D skin;
	public bool FacingRight = true;  // For determining which way the player is currently facing.
	private Vector3 velocity = Vector3.zero;

	public float runSpeed = 20f;

	float horizontalMove = 0f;
	public bool jump = false;
	public bool jump_Hold = false;
	public bool crouch = false;

	public float jumps_Left, Max_Jumps;
	public float jump_Counter, jump_Time, hang_Counter, hang_Time;
	public bool press_Jump;

	public float dashDistance = 15f;
	bool isDashing;
	bool isAbleToDash;
	float doubleTapTime;
	float dashTimer;
	KeyCode lastKeyCode;

	private void Awake()
	{
		Rigidbody2D = GetComponent<Rigidbody2D>();
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(GroundCheck.position, GroundedRadius);
		Gizmos.DrawWireSphere(CeilingCheck.position, CeilingRadius);
		Gizmos.DrawWireSphere(CornerCheck.position, CornerRadius);
	}

	void Update()
	{
		if (dashTimer > 0)
		{
			isAbleToDash = false;
			dashTimer -= Time.deltaTime;
		}
		horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

		if (Input.GetButtonDown("Jump"))
		{
			jump = true;
		}

		if (Input.GetButton("Jump"))
		{
			jump_Hold = true;
		}
		else
		{
			jump_Hold = false;
		}

		if (Input.GetButtonDown("Crouch"))
		{
			crouch = true;
		}
		else if (Input.GetButtonUp("Crouch"))
		{
			crouch = false;
		}

		//dashing left
		if (Input.GetKeyDown(KeyCode.A))
		{
			if ((doubleTapTime > Time.time && lastKeyCode == KeyCode.A) && isAbleToDash)
			{
				StartCoroutine(Dash(-1f));
			}
			else
			{
				doubleTapTime = Time.time + 0.5f;
			}
			lastKeyCode = KeyCode.A;
		}
		//dashing right
		if (Input.GetKeyDown(KeyCode.D))
		{
			if ((doubleTapTime > Time.time && lastKeyCode == KeyCode.D) && isAbleToDash)
			{
				StartCoroutine(Dash(1f));
			}
			else
			{
				doubleTapTime = Time.time + 0.5f;
			}
			lastKeyCode = KeyCode.D;
		}
		Grounded = false;

		// The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
		// This can be done using layers instead but Sample Assets will not overwrite your project settings.
		Collider2D[] colliders = Physics2D.OverlapCircleAll(GroundCheck.position, GroundedRadius, WhatIsGround);
		for (int i = 0; i < colliders.Length; i++)
		{
			if (colliders[i].gameObject != gameObject)
			{
				Grounded = true;
			}
		}

		Ledge_Grab = false;

		Collider2D[] Corner = Physics2D.OverlapCircleAll(CornerCheck.position, CornerRadius, WhatIsGround);
		for (int i = 0; i < Corner.Length; i++)
		{
			if (Corner[i].gameObject != gameObject)
			{
				Ledge_Grab = true;
			}
		}
		if (Wall_Slide)
		{
			//Ledge_Grab = true;
		}
	}
	public void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 8)
		{
			if (Rigidbody2D.velocity.y < -2f)
			{
				Rigidbody2D.gravityScale = 1;
				Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0);
			}
		}
	}
	public void OnCollisionStay2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 8)
		{
			if (Rigidbody2D.velocity.y < -2f)
			{
				Rigidbody2D.gravityScale = 0;
				Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, -3f);
				Wall_Slide = true;
			}
			else
			{
				Rigidbody2D.gravityScale = 2;
				Wall_Slide = false;
			}
		}
	}
	public void OnCollisionExit2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 8)
		{
			Rigidbody2D.gravityScale = 2;
			Wall_Slide = false;
		}
	}

	public void Move(float move, bool crouch, bool jump, bool jump_Hold, bool Grounded, bool Ledge_Grab, bool Wall_Slide)
	{
		if (Grounded)
		{
			hang_Counter = hang_Time;
			if (dashTimer <= 0)
			{
				//isAbleToDash = true;
			}
			jumps_Left = Max_Jumps;
		}
		else
		{
			hang_Counter -= Time.deltaTime;
		}

		// If crouching, check to see if the character can stand up
		if (!crouch)
		{
			// If the character has a ceiling preventing them from standing up, keep them crouching
			if (Physics2D.OverlapCircle(CeilingCheck.position, CeilingRadius, WhatIsGround))
			{
				crouch = true;
			}
		}

		//only control the player if grounded or airControl is turned on
		if (hang_Counter > 0 || AirControl)
		{
			if (Grounded || Ledge_Grab)
			{
				// If crouching
				if (crouch)
				{
					// Reduce the speed by the crouchSpeed multiplier
					move *= CrouchSpeed;
					sprite.transform.localScale = new Vector2(sprite.transform.localScale.x, 0.7f);
					// Disable one of the colliders when crouching
					if (CrouchDisableCollider != null)
						CrouchDisableCollider.enabled = false;
				}
				else
				{
					sprite.transform.localScale = new Vector2(sprite.transform.localScale.x, 1f);
					// Enable the collider when not crouching
					if (CrouchDisableCollider != null)
						CrouchDisableCollider.enabled = true;
				}
			}
			else
			{
				sprite.transform.localScale = new Vector2(sprite.transform.localScale.x, 1f);
				// Enable the collider when not crouching
				if (CrouchDisableCollider != null)
					CrouchDisableCollider.enabled = true;
			}

			// Move the character by finding the target velocity
			Vector3 targetVelocity = new Vector2(move * 10f, Rigidbody2D.velocity.y);
			// And then smoothing it out and applying it to the character
			Rigidbody2D.velocity = Vector3.SmoothDamp(Rigidbody2D.velocity, targetVelocity, ref velocity, MovementSmoothing);

			// If the input is moving the player right and the player is facing left...
			if (move > 0 && !FacingRight)
			{
				// ... flip the player.
				Flip();
			}
			// Otherwise if the input is moving the player left and the player is facing right...
			else if (move < 0 && FacingRight)
			{
				// ... flip the player.
				Flip();
			}
		}


		if (press_Jump)
		{
			// If the player should jump, short press short hop and long press long jump...
			if ((hang_Counter > 0 || Ledge_Grab || jumps_Left > 0) && jump && !crouch)
			{
				Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0f);
				this.Grounded = false;
				Rigidbody2D.AddForce(new Vector2(0f, JumpForce));
				jump_Counter = jump_Time;
				if (hang_Counter < 0)
				{
					jumps_Left--;
				}
				if (Ledge_Grab)
				{
					jump_Counter = 0;
				}
				hang_Counter = 0;
			}
			if (jump_Hold)
			{
				if (jump_Counter > 0)
				{
					if (jump_Counter != jump_Time)
					{
						Rigidbody2D.AddForce(new Vector2(0f, JumpForce / 4));
					}
					jump_Counter -= Time.deltaTime;
				}
				else
				{
					jump_Counter = 0;
				}
			}
			else
			{
				jump_Counter = 0;
			}
			if (Input.GetButtonUp("Jump"))
			{
				jump_Counter = 0;
			}
		}
	}


	void FixedUpdate()
	{
		// Move our character
		if (!isDashing)
		{
			Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, Rigidbody2D.velocity.y);
		}

		Move(horizontalMove * Time.fixedDeltaTime, crouch, jump, jump_Hold, Grounded, Ledge_Grab, Wall_Slide);

		jump = false;

		//if (!isDashing)
	}

	private void Flip()
	{
		// Switch the way the player is labelled as facing.
		FacingRight = !FacingRight;

		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}

	IEnumerator Dash(float direction)
	{
		isDashing = true;
		isAbleToDash = false;
		dashTimer = 1f;
		Rigidbody2D.velocity = new Vector2(Rigidbody2D.velocity.x, 0f);
		Rigidbody2D.AddForce(new Vector2(dashDistance * direction, 0f), ForceMode2D.Impulse);
		float gravity = Rigidbody2D.gravityScale;
		Rigidbody2D.gravityScale = 0;
		yield return new WaitForSeconds(0.4f);
		isDashing = false;
		Rigidbody2D.gravityScale = gravity;
	}
}
