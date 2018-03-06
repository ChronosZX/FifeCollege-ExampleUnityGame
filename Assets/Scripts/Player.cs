using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_speed = 10;
	public float m_jumpSpeed = 50;
	public float m_blinkDuration = 0.2f;
	public int m_allowedAirJumps = 0;

	// Private Variables
	private float m_blinkEnd = 0;
	private int m_numAirJumps = 0;
	#endregion

	#region Unity Functions
	void Update()
	{
		// Get the animator so we can set various values on it
		Animator animator = GetComponent<Animator>();

		// Check if we are pressing a button to more horizontally
		// This will be a number between -1 and 1 based on whether the
		//    player has been pressing the left or right buttons (or a/d)
		//    It should also work with a controller!
		float horizontal = Input.GetAxis("Horizontal");

		// Get the rigidbody component attached to the Player
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();

		// From the rigidbody, get the current velocity.
		// This is so we can preserve any existing y velocity.
		Vector2 velocity = rigidBody.velocity;

		// Set only the x component of the velocity.
		// Set it to the value of the horizontal axis (-1 to 1)
		//    multiplied by the designer variable m_speed.
		velocity.x = horizontal * m_speed;

		// Set our animator speed value to control running/walking/standing animation
		animator.SetFloat("Speed", Mathf.Abs(velocity.x));

		// Turn left or right based on the velocity
		// Only do this if we actually have a velocity - otherwise we would always return to facing one direction when standing.
		bool flip = velocity.x < 0;
		float flipMult = 1;
		if (flip)
		{
			flipMult = -1;
		}

		if (Mathf.Abs(velocity.x) > 0)
		{
			transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * flipMult, 
				transform.localScale.y, 
				transform.localScale.z);
		}

		// Handle jumping
		// First we need to check if we are touching the ground!

		// Get the collider attached to this player
		Collider2D collider = GetComponent<Collider2D>();

		// Get the LayerMask for the ground layer - we need this for our next
		// function call.
		LayerMask groundLayer = LayerMask.GetMask("Ground");

		// Ask the collider if we are touching the ground layer.
		bool touchingGround = collider.IsTouchingLayers(groundLayer);

		// Set our animator TouchingGround value to control our jumping/standing animation
		animator.SetBool("TouchingGround", touchingGround);

		// Check if the "Jump" button (space) has been pressed down.
		if(Input.GetButtonDown("Jump"))
		{
			// If we are touching the ground,
			//    we can reset our air jump count to 0
			if (touchingGround)
				m_numAirJumps = 0;

			// Normally we are only allowed to jump if we are touching the ground.
			bool allowedToJump = touchingGround;

			// However, if our allowed air jumps are 
			//    higher than our current air jump count
			//    (meaning we have at least 1 jump left)
			//    we are also allowed to jump,
			//    even if we aren't touching the ground!
			if (m_allowedAirJumps > m_numAirJumps)
				allowedToJump = true;

			// If we are in fact touching the ground,
			//    only then do we apply the jump!
			//    Otherwise we ignore it.
			if (allowedToJump)
			{
				// Set our upward velocty
				velocity.y = m_jumpSpeed;

				// If we aren't currently touching the ground, 
				//    we need to add to our air jump count.
				if (!touchingGround)
					++m_numAirJumps;
			}
		}

		// Now just update the rigidbody with this new velocty
		// This will update the rigidbody for both running and jumping!
		rigidBody.velocity = velocity;


		// Blink if we are invulnerable
		HandleBlink();

	}
	#endregion

	#region Public Functions
	// Blinks sprite if invulnerable
	private void HandleBlink()
	{
		// Get the sprite renderer component from the Player's game object
		// This is what we will disable and enable to make the blinking effect
		SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

		// If we should be invulnerable...
		if (GetComponent<HealthPool>().IsInvulnerable())
		{
			// Check if it is time to turn off or on our sprite.
			if (Time.time >= m_blinkEnd)
			{
				// If it is...
				// Set our sprite renderers state to the opposite of
				//     what it currently is
				// In other words - toggle it.
				spriteRenderer.enabled = !spriteRenderer.enabled;

				// Record when we should next toggle our sprite renderer.
				m_blinkEnd = Time.time + m_blinkDuration;
			}
		}
		// If we should NOT be invulnerable...
		else
		{
			// Make sure our sprite is showing!
			spriteRenderer.enabled = true;
		}
	}
	#endregion
}
