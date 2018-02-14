using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_health = 100;
	public float m_speed = 10;
	#endregion

	#region Public Functions
	// Damage the player - may cause death!
	public void Damage(float _damage)
	{
		m_health = m_health - _damage;

		// TODO: Handle death!
	}
	#endregion

	#region Unity Functions
	void Update()
	{
		// Check if we are pressing a button to more horizontally
		float horizontal = Input.GetAxis("Horizontal");
		Rigidbody2D rigidBody = GetComponent<Rigidbody2D>();
		Vector2 velocity = rigidBody.velocity;
		velocity.x = horizontal * m_speed;
		rigidBody.velocity = velocity;
		// TODO: Set animation parameter
	}
	#endregion
}
