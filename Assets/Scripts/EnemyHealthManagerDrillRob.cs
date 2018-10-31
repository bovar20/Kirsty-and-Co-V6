using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthManagerDrillRob : MonoBehaviour {

    public int maxEnemyHealth;

	public int enemyHealth;

	//public GameObject deathEffect;

	public int pointsOnDeath;

    //public GameObject healthBar;

    public GameObject RobBody;

    public PolygonCollider2D poly2d;

	// Use this for initialization
	void Start () {

        enemyHealth = maxEnemyHealth;

        poly2d = GetComponent<PolygonCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0) 
		{
			//Instantiate (deathEffect, transform.position, transform.rotation);
			ScoreManager.AddPoints (pointsOnDeath);
            RobBody.SetActive(false);
            poly2d.enabled = false;
        } else if (enemyHealth >= 1){
            RobBody.SetActive(true);
            poly2d.enabled = true;
        }
	}

	public void giveDamage(int damageToGive)
	{
		enemyHealth -= damageToGive;
	}
}
