using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPool : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_health = 100;
	public float m_invulnDuration = 0;
	public bool m_destroyOnDeath = true;

	private float m_invulnEnd = 0;
	#endregion

	#region Public Functions
	// Damage the player - may cause death!
	public void Damage(float _damage)
	{
		// Don't deal damage if we are invulnerable
		// (Check current time against what time it should be to
		//    stop being invulnerable)
		if (!IsInvulnerable())
		{
			// Reduce our health by the damage taken
			m_health = m_health - _damage;

			// Record when our invulnerability should end.
			// This makes us become invulnerable for m_invulnDuration seconds
			m_invulnEnd = Time.time + m_invulnDuration;

			// TODO: Effects for taking damage
			//		- Animation
			//		- Sound

			// If our health has dropped to 0 or lower, we are dead!
			if (m_health <= 0)
			{
				// If we have marked that we should destroy this object when it dies...
				if (m_destroyOnDeath)
				{
					// Destroy the object
					Destroy(gameObject);
				}

				// TODO: Effects for death
				//		- Animation
				//		- Sound
			}
		}
	}

	public bool IsInvulnerable()
	{
		return Time.time < m_invulnEnd;
	}
	#endregion
}
