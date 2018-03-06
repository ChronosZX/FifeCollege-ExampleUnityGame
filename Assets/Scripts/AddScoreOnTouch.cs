using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScoreOnTouch : MonoBehaviour 
{
	#region Variables
	// Exposed Variables
	public int m_scoreToAdd = 10;
	#endregion

	// Called when our collider overlaps with another
	void OnTriggerEnter2D(Collider2D _other)
	{
		// Get the ScoreGetter script from the other object we collided with
		ScoreGetter scoreGetter = _other.GetComponent<ScoreGetter>();

		// Make sure that object HAD a ScoreGetter on it...
		if (scoreGetter != null)
		{
			// Add our score value to the score getter
			scoreGetter.AddScore(10);

			// Get rid of our game object
			Destroy(gameObject);

			// TODO: Effects for the score add
			//		- Animation
			//		- Sound
		}
	}
}
