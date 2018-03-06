using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnTouch : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_damage = 10;
	#endregion


	#region Unity Functions
	// When a trigger interaction occurs involving this game object...
	void OnCollisionStay2D(Collision2D _collision)
	{
		HealthPool healthPool = _collision.collider.GetComponent<HealthPool>();
		if (healthPool != null)
		{
			healthPool.Damage(m_damage);
		}
	}
	#endregion
}
