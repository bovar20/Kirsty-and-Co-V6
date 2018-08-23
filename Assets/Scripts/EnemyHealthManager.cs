using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManager : MonoBehaviour {

    public int maxEnemyHealth;

	public int enemyHealth;

    public GameObject enemyTide;

	//public GameObject deathEffect;

	public int pointsOnDeath;

    public GameObject healthBar;

	// Use this for initialization
	void Start () {

        enemyHealth = maxEnemyHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0) 
		{
			//Instantiate (deathEffect, transform.position, transform.rotation);
			ScoreManager.AddPoints (pointsOnDeath);
            Destroy (enemyTide);
		}
        healthBar.GetComponent<Slider>().value = enemyHealth;
	}

	public void giveDamage(int damageToGive)
	{
		enemyHealth -= damageToGive;
	}
}
