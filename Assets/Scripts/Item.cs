using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {

	// Exposed Variables
	public bool m_destroyOnUse = true;
	public string m_itemName = "";

	// A virtual function means it can be re-defined in another script that extends this one.
	public virtual bool UseItem(GameObject _player)
	{
		// empty
		return true;
	}


	// When a trigger interaction occurs involving this game object...
	void OnTriggerEnter2D(Collider2D _collider)
	{
		// Get the inventory from our collided object
		Inventory inventory = _collider.GetComponent<Inventory>();

		// If it had a inventory...
		if (inventory != null)
		{
			// Turn off the collider on this item so it can't be picked up again
			GetComponent<Collider2D>().enabled = false;

			// Add the item to the inventory
			inventory.AddItem(m_itemName, gameObject);

			// TODO: Effects for the item add
			//		- Animation
			//		- Sound
		}
	}
}
