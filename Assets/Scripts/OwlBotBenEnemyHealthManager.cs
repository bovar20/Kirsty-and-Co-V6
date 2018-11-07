using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBotBenEnemyHealthManager : MonoBehaviour {

    public int maxEnemyHealth;

    public int enemyHealth;

    //public GameObject deathEffect;

    public int pointsOnDeath;

    //public GameObject healthBar;

    public LevelManager levelMan;

    public int damageToGive; //How much this object gives dameage to the player

    public float FlickerTime;

    public float hide_time;

    private float m_DamageFlickerTime;

    public bool Flicker = false;

    // Use this for initialization
    void Start()
    {
        m_DamageFlickerTime = 1f;

        FlickerTime = 0;

        hide_time = 0;

        levelMan = FindObjectOfType<LevelManager>();

        enemyHealth = maxEnemyHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Flicker)
        {
            FlickerTime += Time.deltaTime;
            hide_time += Time.deltaTime;

            if (FlickerTime >= 0.15)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else if (FlickerTime <= 0.14)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
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
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
        }

        if (enemyHealth <= 0)
        {
            //Instantiate (deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            gameObject.SetActive(false);
            Flicker = false;
            FlickerTime = 0;
            hide_time = 0;
        }
        else if (levelMan.OwlBotRespawn)
        {
            enemyHealth = maxEnemyHealth;
            gameObject.SetActive(true);
            StartCoroutine("RespawnReset");
        }
    }

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
    }

    public IEnumerator RespawnReset()
    {
        yield return new WaitForSeconds(1);
        levelMan.OwlBotRespawn = false;
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
