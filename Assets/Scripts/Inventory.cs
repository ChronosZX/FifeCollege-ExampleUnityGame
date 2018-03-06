using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour 
{
	// -------------------------------------------------------------------------------------------

	// Exposed variables
	public int m_maxItems = 10;
	public GameObject m_selector;

	// -------------------------------------------------------------------------------------------

	// Private variables
	private List<string> m_items = new List<string>();
	private List<GameObject> m_itemObjects = new List<GameObject>();
	private int m_selectedItem = 0;

	// -------------------------------------------------------------------------------------------

	// Adds an item to the inventory. Returns true if it was successfully added, false if not
	public bool AddItem(string _itemName, GameObject _itemObject)
	{
		// If we are not uet at the max number of items...
		if (m_items.Count < m_maxItems)
		{
			// Add the item to the list
			m_items.Add(_itemName);
			m_itemObjects.Add(_itemObject);

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

	// Checks if an item is in the inventory - returns true if it is, false if not
	public bool HasItem(string _itemName)
	{
		// Call the Contains function for the item list
		return m_items.Contains(_itemName);
	}

	// -------------------------------------------------------------------------------------------

	// Removes an item from the inventory - returns true if it was there and removed, false if not
	public bool RemoveItem(string _itemName)
	{
		// If the item is present in the inventory...
		if (HasItem(_itemName))
		{
			// Get the index of the item
			int itemIndex = m_items.IndexOf(_itemName);

			// Remove the item from the name list
			m_items.Remove(_itemName);

			// Remove the item from the object list, and destroy it
			Destroy(m_itemObjects[itemIndex]);
			m_itemObjects.RemoveAt(itemIndex);

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
		if (m_selectedItem < m_items.Count)
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
			string itemName = m_items[m_selectedItem];

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
