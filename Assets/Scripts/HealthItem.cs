using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We put "Item" after the : so that HealthItem IS an Item it gets everything the Item script gets, and adds to it.
public class HealthItem : Item {
	
	// Exposed Variables
	public float m_healing = 10;

	// override means this function re-defines a function on it's parent script.
	public override bool UseItem(GameObject _player)
	{
		// Get the health pool from our collided object
		HealthPool healthPool = _player.GetComponent<HealthPool>();

		// If it had a health pool...
		if (healthPool != null)
		{
			// Heal it by the amount set
			healthPool.Heal(m_healing);

			// TODO: Effects for healing
			//		- Animation
			//		- Sound

			return true;
		}
		else
		{
			return false;
		}
	}

}
