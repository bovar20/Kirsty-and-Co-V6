using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickUp : MonoBehaviour {

	public int pointsToAdd;

	//public AudioSource biscuitSoundEffect;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<CharacterController2D> () == null)
			return;
		
		ScoreManager.AddPoints (pointsToAdd);

		//biscuitSoundEffect.Play ();

		Destroy (gameObject);

	}

}
