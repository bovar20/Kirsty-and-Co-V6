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

    // Use this for initialization
    void Start()
    {

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
            gameObject.SetActive(false);
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
}
