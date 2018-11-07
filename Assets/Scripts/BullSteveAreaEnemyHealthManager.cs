using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullSteveAreaEnemyHealthManager : MonoBehaviour
{

    public int maxEnemyHealth;

    public int enemyHealth;

    //public GameObject deathEffect;

    public int pointsOnDeath;

    //public GameObject healthBar;

    public LevelManager levelMan;

    public GameObject BullStevesbody;

    public BoxCollider2D BoxCol2d;

    public GameObject RespawnBull;

    public float FlickerTime;

    public float hide_time;

    private float m_DamageFlickerTime;

    public bool Flicker = false;

    public int damageToGive; //How much this object gives dameage to the player

    // Use this for initialization
    void Start()
    {
        BoxCol2d = GetComponent<BoxCollider2D>();

        levelMan = FindObjectOfType<LevelManager>();

        enemyHealth = maxEnemyHealth;

        m_DamageFlickerTime = 1f;
        FlickerTime = 0;
        hide_time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyHealth <= 0)
        {
            //Instantiate (deathEffect, transform.position, transform.rotation);
            ScoreManager.AddPoints(pointsOnDeath);
            BullStevesbody.SetActive(false);
            BoxCol2d.enabled = false;
            hide_time = 0;
            FlickerTime = 0;
            Flicker = false;
        }
        if (levelMan.BullSteveAreaRespawn)
        {
            enemyHealth = maxEnemyHealth;
            BullStevesbody.SetActive(true);
            BoxCol2d.enabled = true;
            transform.position = RespawnBull.transform.position;
            StartCoroutine("RespawnReset");
        }

        if (Flicker)
        {
            FlickerTime += Time.deltaTime;
            hide_time += Time.deltaTime;

            if (FlickerTime >= 0.15)
            {
                BullStevesbody.SetActive(false);
            }
            else if (FlickerTime <= 0.14)
            {
                BullStevesbody.SetActive(true);
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
                BullStevesbody.SetActive(true);
            }
        }
    }

    public void giveDamage(int damageToGive)
    {
        enemyHealth -= damageToGive;
    }

    public IEnumerator RespawnReset()
    {
        yield return new WaitForSeconds(1);
        levelMan.BullSteveAreaRespawn = false;
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

