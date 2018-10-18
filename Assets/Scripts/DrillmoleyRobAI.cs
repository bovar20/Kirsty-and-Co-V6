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


    // Use this for initialization
    void Start()
    {
        runspeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {

        hittingWall = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);
       

        moveSpeed = runspeed;
        if (hittingWall)
        {
            moveRight = !moveRight;
        }

        if (moveRight)
        {
            transform.localScale = new Vector3(-3.326524f, 3.326524f, 3.326524f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
        else
        {
            transform.localScale = new Vector3(3.326524f, 3.326524f, 3.326524f);
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
        }
    }
}
