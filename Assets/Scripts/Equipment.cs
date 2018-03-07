using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// We put "Item" after the : so that Equipment IS an Item it gets everything the Item script gets, and adds to it.
public class Equipment : Item {

	// Exposed Variables
	public string m_slot = "";

	// override means this function re-defines a function on it's parent script.
	public override bool UseItem(GameObject _player)
	{
		EquippedItems equipped = _player.GetComponent<EquippedItems>();

		if (equipped != null)
		{
			equipped.
		}

		// empty
		return true;
	}

	// A virtual function means it can be re-defined in another script that extends this one.
	public virtual bool EquipItem(GameObject _player)
	{
		// empty
		return true;
	}

	// A virtual function means it can be re-defined in another script that extends this one.
	public virtual bool UnEquipItem(GameObject _player)
	{
		// empty
		return true;
	}
}
