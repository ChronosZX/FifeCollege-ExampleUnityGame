using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItems : MonoBehaviour 
{
	// -------------------------------------------------------------------------------------------

	// Exposed variables
	public List<string> m_slotNames = new List<string>();
	public List<GameObject> m_equipmentRoots = new List<GameObject>();

	// -------------------------------------------------------------------------------------------

	// Private variables
	private List<string> m_equippedSlots = new List<string>();
	private List<GameObject> m_equipmentObjects = new List<GameObject>();

	// -------------------------------------------------------------------------------------------

	// Adds an item to the inventory. Returns true if it was successfully added, false if not
	public bool Equip(string _slotName, GameObject _equipmentObject, bool replace = true)
	{
		// First, check if the slot is present in our slot list
		if (!m_slotNames.Contains(_slotName))
		{
			// If not, print an error and exit the function early.
			Debug.LogError("Couldn't find slot "+_slotName);
			return false;
		}

		// If replace is true, remove anything currently in the slot
		if (replace == true)
		{
			RemoveSlot(_slotName);
		}

		// If we still have something equipped, we have failed to equip this one.
		if (HasEquippedSlot(_slotName) == true)
		{
			// do nothing and return false
			return false;
		}
		// If we either DON'T have an item in this slot OR we SHOULD replace it...
		else
		{
			if (alreadyEquippedSlot == true)
			{
				RemoveSlot(_slotName);
			}

			// Add the item to the list
			m_equippedSlots.Add(_slotName);
			m_equipmentObjects.Add(_equipmentObject);

			// Add the item visually to the inventory
			UpdateVisuals();

			// Return true since we added it
			return true;
		}
		else
		{
			// Otherwise, return false since we failed to add it
			return false;
		}
	}

	// -------------------------------------------------------------------------------------------

	// Checks if a slot has an item equipped in it - returns true if it does, false if not
	public bool HasEquippedSlot(string _slotName)
	{
		// Call the Contains function for the item list
		return m_slotNames.Contains(_slotName);
	}

	// -------------------------------------------------------------------------------------------

	// Removes any equipment in the given slot - returns true if it was there and removed, false if not
	public bool RemoveSlot(string _slotName)
	{
		// If the item is present in the inventory...
		if (HasEquippedSlot(_slotName))
		{
			// Get the index of the slot
			int slotIndex = m_equippedSlots.IndexOf(_slotName);

			// Remove the slot from the list of equipped slots
			m_equippedSlots.Remove(_slotName);

			// Destroy and remove the equipment object
			Destroy(m_equipmentObjects[slotIndex]);
			m_equipmentObjects.RemoveAt(slotIndex);

			// Remove the item visually from the list
			UpdateVisuals();

			// Return true since we removed the item
			return true;
		}
		else
		{
			// If it isnt in the inventory, return false
			return false;
		}
	}

	// -------------------------------------------------------------------------------------------

	// Uses an item from the inventory
	public bool UseCurrentItem()
	{
		// If we are selecting a current item
		if (m_selectedItem < m_itemNames.Count)
		{
			// boolean to store if we should remove this item or not
			// This defaults to false (dont remove it)
			bool shouldRemove = false;
			bool wasUsed = false;

			// Get the Item script from the selected itemObject
			Item item = m_itemObjects[m_selectedItem].GetComponent<Item>();

			// If there was indeed an Item script attached...
			if (item != null)
			{
				// Use the item - it will return if it was used
				wasUsed = item.UseItem(gameObject);

				// If the item should be destroyed when used, and it was in fact used....
				if (item.m_destroyOnUse && wasUsed)
				{
					// set it as needing to be removed.
					shouldRemove = true;
				}
			}

			// Get the item's name
			string itemName = m_itemNames[m_selectedItem];

			// Remove the item, if we've decided we should
			if (shouldRemove)
			{
				RemoveItem(itemName);
			}

			// return whether or not we successfully used the item
			return wasUsed;
		}
		else
		{
			// return failure
			return false;
		}
	}

	// -------------------------------------------------------------------------------------------

	// Update the visual representation of the items
	public void UpdateVisuals()
	{
		// for each itemObject in the itemObjext list...
		for (int i = 0; i < m_itemObjects.Count; ++i)
		{
			// Update the object's position to match the root position
			m_itemObjects[i].transform.position = m_itemRoots[i].transform.position;
		}
	}

	// -------------------------------------------------------------------------------------------

	// Set the currently selected item to the given slot
	public void SetSelection(int _newSelection)
	{
		// If the new selection is greater than 0 and less than the maximum number of items...
		if (_newSelection >= 0 && _newSelection < m_maxItems)
		{
			// Set our selected item variable to _newSelection
			m_selectedItem = _newSelection;

			// Visually set our selector to our selected item
			m_selector.transform.position = m_itemRoots[m_selectedItem].transform.position;
		}
	}

	// -------------------------------------------------------------------------------------------

	// Set the currently selected item to the given slot
	public void ChangeSelection(int _toChange)
	{
		// Our new selection is equal to our current selection, plus the ammount to change - modded by the number of max items.
		// Mod means that if the number is higher than the mod number (maxItems), it wraps around
		// This means that if we have the last slot selected and try to select the next slot, it will wrap around
		// to the first slot.
		int newSelection = (m_selectedItem + _toChange) % m_maxItems;

		// Now just use our SetSelection function to set the selection to this value
		SetSelection(newSelection);
	}

	// -------------------------------------------------------------------------------------------

	// Unity function called once per frame
	void Update()
	{
		// If the player has just pressed the "UseItem" button (set up in the Input settings in Unity
		if (Input.GetButtonDown("UseItem"))
		{
			// Use the currently selected item
			UseCurrentItem();
		}
	}

	// -------------------------------------------------------------------------------------------
}
