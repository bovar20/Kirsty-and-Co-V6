using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthManager : MonoBehaviour
{

    public int maxPlayerHealth;

    public static int playerHealth;

    //Text text;

    public Slider healthBar;

    private LevelManager levelManager;

    public bool isDead;

    //private LifeManager lifeSystem;

    //private TimeManager theTime;

    // Use this for initialization
    void Start()
    {
        //text = GetComponent<Text> ();

        healthBar = GetComponent<Slider>();

        playerHealth = maxPlayerHealth;

        levelManager = FindObjectOfType<LevelManager>();

        //lifeSystem = FindObjectOfType<LifeManager>();

       //theTime = FindObjectOfType<TimeManager>();

        isDead = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (playerHealth <= 0 && !isDead)
        {
            playerHealth = 0;
            levelManager.RespawnPlayer();
            //lifeSystem.TakeLife();
            isDead = true;

        }
        if (playerHealth > maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }


        healthBar.value = playerHealth;
        //text.text = "" + playerHealth;
    }

    public static void HurtPlayer(int damageToGive)
    {
        playerHealth -= damageToGive;
    }

    public void FullHealth()
    {
        playerHealth = maxPlayerHealth;
    }

    public void KillPlayer()
    {
        playerHealth = 0;
    }
}

