// *********************************************************************************************************************
// File: AddCurrencyOnDeath.cs
// Purpose: Adds currency to a CurrencyPouch when this object dies
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
public class AddCurrencyOnDeath : MonoBehaviour {
// *********************************************************************************************************************


	// *****************************************************************************************************************
	#region Variables
	// *****************************************************************************************************************
	// Exposed Variables
	[Tooltip("What type of currency should be added?")]
	public string m_currencyType = "score";
	[Tooltip("How much currency will be added?")]
	public int m_amount = 1;
	[Tooltip("When this health pool dies, we'll add currency")]
	public HealthPool m_healthPool;
	[Tooltip("The CurrencyPouch to which this currency should be added")]
	public CurrencyPouch m_currencyPouch = null;
	#endregion
	// *****************************************************************************************************************


	// *****************************************************************************************************************
	#region Unity Functions
	// *****************************************************************************************************************
	// Called once per frame
	void Update()
	{
		// If the health pool has died...
		if (m_healthPool.IsAlive() == false)
		{
			// Attempt to add to the CurrencyPouch
			m_currencyPouch.AddCurrency(m_currencyType, m_amount);
		}
	}
	#endregion
	// *****************************************************************************************************************


}
// *********************************************************************************************************************
