using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillmoelyHollyBossAI : MonoBehaviour {


    public bool Stance = false;
    public bool Running = false;
    public bool WhirlAtk = false;
    public bool RunningLeft = false;
    public bool RunningRight = false;
    public float moveSpeed;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public bool hittingWall;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Stance", Stance);
        anim.SetBool("Running", Running);
        anim.SetBool("WhirlAtk", WhirlAtk);

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (RunningRight){
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (hittingWall && RunningRight){
            transform.localScale = new Vector3(-1f, 1f, 1f);
            RunningRight = false;
            StartCoroutine("StanceRight");
        }

        if (RunningLeft)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }

        if (hittingWall && RunningLeft)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            RunningLeft = false;
            StartCoroutine("StanceLeft");
        }
    }


    IEnumerator StartingStance ()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("This is working");
        StartCoroutine("WhirlwindattackLeft");

    }

    IEnumerator WhirlwindattackLeft ()
    {
        WhirlAtk = true;
        yield return new WaitForSeconds(3.4f);
        WhirlAtk = false;
        Running = true;
        RunningRight = true;
    }

    IEnumerator StanceRight()
    {
        Running = false;
        Stance = true;
        yield return new WaitForSeconds(1f);
        Stance = false;
        StartCoroutine("WhirlwindattackRight");
    }
     
    IEnumerator WhirlwindattackRight()
    {
        WhirlAtk = true;
        yield return new WaitForSeconds(3.4f);
        WhirlAtk = false;
        RunningLeft = true;
        Running = true;
    }

    IEnumerator StanceLeft()
    {
        Running = false;
        Stance = true;
        yield return new WaitForSeconds(1f);
        Stance = false;
        StartCoroutine("WhirlwindattackLeft");
    }
}
