using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeHealth : MonoBehaviour {

    public int maxTreeHealth;

    public int treeHealth;

    public GameObject TreeTide;

    //public GameObject deathEffect;

    //public int pointsOnDeath;

    //public GameObject healthBar;

    public GameObject Stump;

    public GameObject treeLog;

    public Animator anim;

    // Use this for initialization
    void Start()
    {

        treeHealth = maxTreeHealth;
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetInteger("TreeHealthAnim", treeHealth);
        if (treeHealth <= 0)
        {
            //Instantiate (deathEffect, transform.position, transform.rotation);
            //ScoreManager.AddPoints(pointsOnDeath);
            Stump.SetActive(true);
            treeLog.SetActive(true);
            Destroy(TreeTide);
        }
        //healthBar.GetComponent<Slider>().value = treeHealth;
    }

    public void giveDamage(int damageToGive)
    {
        treeHealth -= damageToGive;
    }
}

