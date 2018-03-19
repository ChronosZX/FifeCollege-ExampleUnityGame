// *********************************************************************************************************************
// File: AddCurrencyOnTouch.cs
// Purpose: Adds currency to a CurrencyPouch that touches this trigger
// Project: Fife College Unity Toolkit
// Copyright Fife College 2018
// *********************************************************************************************************************


// *********************************************************************************************************************
#region Imports
// *********************************************************************************************************************
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#endregion
// *********************************************************************************************************************


// *********************************************************************************************************************
[RequireComponent(typeof(Collider2D))]
public class AddCurrencyOnTouch : MonoBehaviour {
	// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("What type of currency should be added?")]
	public string m_currencyType = "score";
	[Tooltip("How much currency will be added?")]
	public int m_amount = 1;
	[Tooltip("Should we destroy this object after collecting it?")]
	public bool m_destroyWhenAdded = true;
	[Tooltip("The sound that should be played when this currency is added")]
	public AudioClip m_currencyAddSound = null;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// When a trigger interaction starts involving this game object...
	void OnTriggerEnter2D(Collider2D _other)
	{
		// Check if the other collider that we hit has a CurrencyPouch on it
		CurrencyPouch currencyPouch = _other.GetComponent<CurrencyPouch>();
		if (currencyPouch != null) {
			// Attempt to add to the CurrencyPouch
			bool added = currencyPouch.AddCurrency(m_currencyType, m_amount);

			// if we successfully added it...
			if (added) {
				// If we should destroy this object, do so
				if (m_destroyWhenAdded) {
					Destroy (gameObject);
				}

				// If we have a sound to play, play it
				if (m_currencyAddSound) {
					// Play the sound for currency add at this locaiton
					AudioSource.PlayClipAtPoint(m_currencyAddSound,transform.position);
				}
			}
		}
	}
	// *****************************************************************************************************************
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
