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

    public float FlickerTime;

    public float hide_time;

    private float m_DamageFlickerTime;

    public bool Flicker = false;

    public int damageToGive; //How much this object gives dameage to the player

    // Use this for initialization
    void Start () {

        enemyHealth = maxEnemyHealth;

        poly2d = GetComponent<PolygonCollider2D>();

        m_DamageFlickerTime = 1f;
        FlickerTime = 0;
        hide_time = 0;
    }
	
	// Update is called once per frame
	void Update () {
		if (enemyHealth <= 0) 
		{
			//Instantiate (deathEffect, transform.position, transform.rotation);
			ScoreManager.AddPoints (pointsOnDeath);
            RobBody.SetActive(false);
            poly2d.enabled = false;
            Flicker = false;
            FlickerTime = 0;
            hide_time = 0;
        } else if (enemyHealth >= 1){
            RobBody.SetActive(true);
            poly2d.enabled = true;
        }

        if (Flicker)
        {
            FlickerTime += Time.deltaTime;
            hide_time += Time.deltaTime;

            if (FlickerTime >= 0.15)
            {
                RobBody.SetActive(false);
            }
            else if (FlickerTime <= 0.14)
            {
                RobBody.SetActive(true);
            }
            if (FlickerTime >= 0.30)
            {
                FlickerTime = 0;
            }

            if (hide_time >= m_DamageFlickerTime)
            {
                hide_time = 0;
                FlickerTime = 0;
                Flicker = false;
                RobBody.SetActive(true);
            }
        }
    }

	public void giveDamage(int damageToGive)
	{
		enemyHealth -= damageToGive;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "projectiles" && !Flicker)
        {
            giveDamage(damageToGive);
            Flicker = true;
        }
    }
}
