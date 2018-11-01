using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayerOnContact : MonoBehaviour
{

    public int damageToGive;

    public PlayerMovement playermove;

    public Collider2D col2d;

    // Use this for initialization
    void Start()
    {
        playermove = FindObjectOfType<PlayerMovement>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Kirsty" && !playermove.player_hit)
        {
            HealthManager.HurtPlayer(damageToGive);

            playermove.player_hit = true;

            gameObject.GetComponent<Collider2D>().enabled = false;

            StartCoroutine("EyeFrames");

            //var player = other.GetComponent<PlayerController>();
            //player.knockbackCount = player.knockbackLegth;

            //if (other.transform.position.x < transform.position.x)
            //player.knockFromRight = true;
            //else
            //player.knockFromRight = false;
        }
    }
    private IEnumerator EyeFrames(){
        yield return new WaitForSeconds(3);
        gameObject.GetComponent<Collider2D>().enabled = true;
    }
}

