using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillmoleyHollyEnemyHealth : MonoBehaviour {

    public int maxEnemyHealth;

    public int enemyHealth;

    //public GameObject deathEffect;

    public int pointsOnDeath;

    //public GameObject healthBar;

    public GameObject HollyBody;

    public BoxCollider2D box2d;

    public float FlickerTime;

    public float hide_time;

    private float m_DamageFlickerTime;

    public bool Flicker = false;

    public int damageToGive; //How much this object gives dameage to the player

    public GameObject HollyWhole;

    public GameObject HollyDeath;

    // Use this for initialization
    void Start()
    {

        enemyHealth = maxEnemyHealth;

        box2d = GetComponent<BoxCollider2D>();

        m_DamageFlickerTime = 1f;
        FlickerTime = 0;
        hide_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            Instantiate (HollyDeath, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            Destroy(HollyWhole);
        }
        else if (enemyHealth >= 1)
        {
            HollyBody.SetActive(true);
            box2d.enabled = true;
        }

        if (Flicker)
        {
            FlickerTime += Time.deltaTime;
            hide_time += Time.deltaTime;

            if (FlickerTime >= 0.15)
            {
                HollyBody.SetActive(false);
            }
            else if (FlickerTime <= 0.14)
            {
                HollyBody.SetActive(true);
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
                HollyBody.SetActive(true);
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
