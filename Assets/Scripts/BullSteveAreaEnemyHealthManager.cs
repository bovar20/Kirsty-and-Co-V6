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

    // Use this for initialization
    void Start()
    {
        BoxCol2d = GetComponent<BoxCollider2D>();

        levelMan = FindObjectOfType<LevelManager>();

        enemyHealth = maxEnemyHealth;
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
        }
        if (levelMan.BullSteveAreaRespawn)
        {
            enemyHealth = maxEnemyHealth;
            BullStevesbody.SetActive(true);
            BoxCol2d.enabled = true;
            transform.position = RespawnBull.transform.position;
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
        levelMan.BullSteveAreaRespawn = false;
    }
}

