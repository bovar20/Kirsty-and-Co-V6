using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillmoleyRobAI : MonoBehaviour {

    public float moveSpeed;
    public float runspeed;
    public bool moveRight;

    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public bool hittingWall;

    public bool spotted = false;

    private bool spottedline = false;

    public Transform sightStart, sightEnd;

    private Animator anim;

    public Transform StartingPoint;

    public LevelManager levelMan;

    public EnemyHealthManagerDrillRob enemyH;

    public GameObject RobsBody;

    // Use this for initialization
    void Start()
    {
        runspeed = moveSpeed;
        anim = GetComponent<Animator>();
        levelMan = FindObjectOfType<LevelManager>();
        enemyH = GetComponent<EnemyHealthManagerDrillRob>();
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("RobisRunning", spotted);

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        Debug.DrawLine(sightStart.position, sightEnd.position, Color.blue);

        spottedline = Physics2D.Linecast(sightStart.position, sightEnd.position, 1 << LayerMask.NameToLayer("Player"));

        moveSpeed = runspeed;

        if(spottedline){
            spotted = true;
        }

        if(spotted) {
            if (hittingWall)
            {
                moveRight = !moveRight;
            }

            if (moveRight)
            {
                transform.localScale = new Vector3(3.067368f, 3.067368f, 3.067368f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                transform.localScale = new Vector3(-3.067368f, 3.067368f, 3.067368f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
        }

        if (levelMan.DrillmoleyRobRespawnPos){
            transform.position = StartingPoint.position;
            RobsBody.transform.position = StartingPoint.position;
            spotted = false;
            enemyH.enemyHealth = enemyH.maxEnemyHealth;
            moveRight = false;
            transform.localScale = new Vector3(-3.067368f, 3.067368f, 3.067368f);
            StartCoroutine ("RespawnReset");
        }
    }

    public IEnumerator RespawnReset()
    {
        yield return new WaitForSeconds(1);
        levelMan.DrillmoleyRobRespawnPos = false;
    }
}
