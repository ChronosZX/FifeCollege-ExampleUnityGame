using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreGetter : MonoBehaviour 
{
	// Exposed data
	public TextMesh m_scoreText;

	// private data
	private int m_score = 0;

	// Public function so other objects can add to our score
	public void AddScore(int _toAdd)
	{
		// Add the score we have been given to our total
		m_score = m_score + _toAdd;

		// Update the text display to match our new total
		m_scoreText.text = m_score.ToString();
	}

}
