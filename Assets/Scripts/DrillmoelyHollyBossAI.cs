using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillmoelyHollyBossAI : MonoBehaviour {


    private bool Stance = false;
    private bool StanceLeft = false;
    private bool StanceRight = false;
    private bool Running = false;
    public bool WhirlAtk = false;
    public bool WhirlAtkRight = false;
    private bool WhirlAtkLeft = false;
    private bool RunningLeft = false;
    private bool RunningRight = false;
    public float moveSpeed = 0f;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public bool hittingWall;
    public GameObject Respawnpoint;

    public float StanceLeftTimerMax = 2f;
    private float StanceLeftTime;
    public float StanceRightTimerMax = 2f;
    private float StanceRightTime;
    public float WhirlAtkLeftTimerMax = 5f;
    private float WhirlAtkLeftTime;
    public float WhirlAtkRightTimerMax = 5f;
    private float WhirlAtkRightTime;
    public bool Respawn = false;
    public GameObject Barrier;
    public GameObject BossRoom;

    private Animator anim;

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();

        StanceLeftTime = StanceLeftTimerMax;
        StanceRightTime = StanceRightTimerMax;
        WhirlAtkLeftTime = WhirlAtkLeftTimerMax;
        WhirlAtkRightTime = WhirlAtkRightTimerMax;
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Stance", Stance);
        anim.SetBool("Running", Running);
        anim.SetBool("WhirlAtk", WhirlAtk);
        anim.SetBool("Respawn", Respawn);

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        if (RunningRight){
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            StanceLeftTime = StanceLeftTimerMax;
        }

        if (hittingWall && RunningRight){
            transform.localScale = new Vector3(-1f, 1f, 1f);
            Stance = true;
            StanceLeft = true;
            RunningRight = false;
            Running = false;
        }

        if (RunningLeft)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            StanceRightTime = StanceRightTimerMax;

        }

        if (hittingWall && RunningLeft)
        {
            transform.localScale = new Vector3(1f, 1f, 1f);
            Stance = true;
            StanceRight = true;
            RunningLeft = false;
            Running = false;
        }

        if(WhirlAtk && WhirlAtkLeft){
            WhirlAtkLeftTime -= Time.deltaTime;

            if (WhirlAtkLeftTime <= 0){
                WhirlAtkLeftTime = 0;
                Running = true;
                RunningLeft = true;
                WhirlAtk = false;
                WhirlAtkLeft = false;
            }
        }

        if (WhirlAtk && WhirlAtkRight){
            WhirlAtkRightTime -= Time.deltaTime;

            if (WhirlAtkRightTime <= 0){
                WhirlAtkRightTime = 0;
                RunningRight = true;
                Running = true;
                WhirlAtk = false;
                WhirlAtkRight = false;
            }
        }

        if (Stance && StanceLeft){
            StanceLeftTime -= Time.deltaTime;
            WhirlAtkLeftTime = WhirlAtkLeftTimerMax;

            if (StanceLeftTime <= 0){
                StanceLeftTime = 0;
                WhirlAtk = true;
                WhirlAtkLeft = true;
                Stance = false;
                StanceLeft = false;
            }
        }

        if (Stance && StanceRight)
        {
            StanceRightTime -= Time.deltaTime;
            WhirlAtkRightTime = WhirlAtkRightTimerMax;

            if (StanceRightTime <= 0){
                StanceRightTime = 0;
                WhirlAtk = true;
                WhirlAtkRight = true;
                Stance = false;
                StanceRight = false;
            }
        }

        if(Respawn){
            transform.position = Respawnpoint.transform.position;
            transform.localScale = new Vector3(1f, 1f, 1f);
            WhirlAtk = false;
            WhirlAtkLeft = false;
            WhirlAtkRight = false;
            Stance = false;
            StanceLeft = false;
            StanceRight = false;
            Running = false;
            RunningLeft = false;
            RunningRight = false;
            BossRoom.SetActive(true);
            Barrier.SetActive(false);
            StanceLeftTime = StanceLeftTimerMax;
            StanceRightTime = StanceRightTimerMax;
            WhirlAtkLeftTime = WhirlAtkLeftTimerMax;
            WhirlAtkRightTime = WhirlAtkRightTimerMax;
        }
    }
}
