using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_health = 100;
	public float m_speed = 10;
	public float m_invulnDuration = 1;
	public float m_blinkDuration = 0.2f;

	// Private Variables
	private float m_invulnEnd = 0;
	private float m_blinkEnd = 0;
	#endregion

	#region Public Functions
	// Damage the player - may cause death!
	public void Damage(float _damage)
	{
		// Don't deal damage if we are invulnerable
		// (Check current time against what time it should be to
		//    stop being invulnerable)
		if (Time.time >= m_invulnEnd)
		{
			// Reduce our health by the damage taken
			m_health = m_health - _damage;

			// Record when our invulnerability should end.
			// This makes us become invulnerable for m_invulnDuration seconds
			m_invulnEnd = Time.time + m_invulnDuration;

			// TODO: Play sound!
			// TODO: Bounce player?
			// TODO: Handle death!
		}
	}
	#endregion

	#region Unity Functions
	void Update()
	{
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

		// Now just update the rigidbody with this new velocty
		rigidBody.velocity = velocity;

		// TODO: Set running animation parameter


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
		if (Time.time < m_invulnEnd)
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
