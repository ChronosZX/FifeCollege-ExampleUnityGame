using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goomba : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public float m_damage = 10;
	#endregion

	#region Unity Functions
	// When a trigger interaction occurs involving this game object...
	void OnTriggerStay2D(Collider2D _other)
	{
		Player player = _other.GetComponent<Player>();
		if (player != null)
		{
			player.Damage(m_damage);
		}
	}
	#endregion
}
