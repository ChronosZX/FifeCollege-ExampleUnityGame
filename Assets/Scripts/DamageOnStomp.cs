using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnStomp : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_damage = 10;
	#endregion


	#region Unity Functions
	// When a trigger interaction occurs involving this game object...
	void OnTriggerStay2D(Collider2D _collider)
	{
		HealthPool healthPool = _collider.GetComponent<HealthPool>();
		if (healthPool != null)
		{
			healthPool.Damage(m_damage);

			// TODO: Effects for the attack
			//		- Animation
			//		- Sound
		}
	}
	#endregion
}
