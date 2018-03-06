using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthOnTouch : MonoBehaviour {

	#region Variables
	// Exposed Variables
	public float m_healing = 10;
	#endregion


	#region Unity Functions
	// When a trigger interaction occurs involving this game object...
	void OnTriggerEnter2D(Collider2D _collider)
	{
		// Get the health pool from our collided object
		HealthPool healthPool = _collider.GetComponent<HealthPool>();

		// If it had a health pool...
		if (healthPool != null)
		{
			// Heal it by the amount set
			healthPool.Heal(m_healing);

			// Get rid of our game object
			Destroy(gameObject);

			// TODO: Effects for the score add
			//		- Animation
			//		- Sound
		}
	}
	#endregion
}
