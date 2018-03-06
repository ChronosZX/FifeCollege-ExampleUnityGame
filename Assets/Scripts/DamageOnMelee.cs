using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnMelee : MonoBehaviour {

	#region Variables
	// Exposed Variables
	public float m_damage = 10;
	public string m_meleeButton = "";
	public Collider2D m_meleeTrigger = null;
	public float m_cooldownDuration = 1f;

	// Private variables
	private float m_cooldownEnd = 0;
	#endregion


	#region Unity Functions
	// When a trigger interaction occurs involving this game object...
	void Update()
	{
		// Don't attack if we are on cooldown
		// (Check current time against what time it should be to
		//    stop being invulnerable)
		// AND only attack if we have pressed the attack button
		if (!IsOnCooldown() && Input.GetButtonDown(m_meleeButton))
		{
			// Check the melee collider trigger for any HealthPool object touching the trigger

			// Get a list of the colliders that are currently overlapping out melee trigger collider
			Collider2D[] colliders = new Collider2D[20];
			m_meleeTrigger.OverlapCollider(new ContactFilter2D(),colliders);

			// For each collider in the list...
			for (int i = 0; i < colliders.Length; ++i)
			{
				// Check if the collider exists
				if (colliders[i] != null)
				{
					// If it exists, get the health pool component off it
					HealthPool healthPool = colliders[i].GetComponent<HealthPool>();
					if (healthPool != null)
					{
						// Apply damage to the health pool
						healthPool.Damage(m_damage);
					}
				}
			}

			// Record when our cooldown should end.
			// This makes us unable to attack for m_cooldownDuration seconds
			m_cooldownEnd = Time.time + m_cooldownDuration;

			// TODO: Effects for the melee attack
			//		- Animation
			//		- Sound
		}
	}

	// Are we on cooldown?
	public bool IsOnCooldown()
	{
		// true if the current time is less than the cooldown end time
		return Time.time < m_cooldownEnd;
	}
	#endregion
}
