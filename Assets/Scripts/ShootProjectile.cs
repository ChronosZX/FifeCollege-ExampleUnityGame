using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public GameObject m_projectilePrefab = null;
	public float m_cooldownDuration = 1f;
	public float m_projectileSpeed = 10;
	public Transform m_firingPoint;

	// Private variables
	private float m_cooldownEnd = 0;
	#endregion
	
	// Update is called once per frame
	void Update () 
	{
		// If we have clicked the mouse button and our attack is not on cooldown...
		if (Input.GetMouseButton(0) && !IsOnCooldown())
		{
			// Determine where the mouse is in the game world
			Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

			// Determine the direction of the mouse relative to our position
			// "normalized" means the vector has a length of 1, 
			//   meaning we can multiply it by a speed to get an initial velocity for our projectile.
			Vector3 direction = (mouseWorldPosition - m_firingPoint.position).normalized;

			// Create a copy of our projectile
			GameObject newProjectile = Instantiate(m_projectilePrefab);

			// Set the projetile's starting position to our fire point
			newProjectile.transform.position = m_firingPoint.position;

			// Get the rigidbody attached to the projectile
			Rigidbody2D projectileBody = newProjectile.GetComponent<Rigidbody2D>();

			// Set our projectile's velocity based on the direction we want it to go
			// and our projectile speed variable
			projectileBody.velocity = direction * m_projectileSpeed;

			// Record when our cooldown should end.
			// This makes us unable to attack for m_cooldownDuration seconds
			m_cooldownEnd = Time.time + m_cooldownDuration;
		}
	}

	// Is our attack on cooldown?
	public bool IsOnCooldown()
	{
		// true if the current time is less than the end time for the cooldown
		return Time.time < m_cooldownEnd;
	}
}
