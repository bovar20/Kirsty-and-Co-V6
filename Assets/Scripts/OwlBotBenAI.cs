using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwlBotBenAI : MonoBehaviour {

    public GameObject Enemy;

    public float moveSpeed;

    public Transform currentPoint;

    public Transform[] points;

    public int pointSelection;

    private bool pointplus;

    private float moveCountdownTotal;

    private float moveCountdownRight;

    private float moveCountdownLeft;

    private float randomShotValue;

    public Transform shotpoint;

    public GameObject projectile;

    public float shotSpeed;

    private bool randomSwitch;

    public Animator anim;

    private bool flyingAnimation;



    // Use this for initialization
    void Start()
    {
        currentPoint = points[pointSelection];

        moveCountdownTotal = 8;

        moveCountdownLeft = moveCountdownTotal;

        moveCountdownRight = moveCountdownTotal;

        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetBool("isFlying", flyingAnimation);

        Enemy.transform.position = Vector3.MoveTowards(Enemy.transform.position, currentPoint.position, Time.deltaTime * moveSpeed);

        if (Enemy.transform.position == currentPoint.position)
        {

            if (pointSelection >= 6)
            {
                pointplus = false;
                moveCountdownRight = moveCountdownTotal;
               
            } else if (pointSelection <= 0){
                pointplus = true;
                moveCountdownLeft = moveCountdownTotal;
            }

            if(pointplus){
                moveCountdownRight -= Time.deltaTime;
            } else if (!pointplus){
                moveCountdownLeft -= Time.deltaTime;
            }

            if(moveCountdownLeft <= 0){
                pointSelection--;

            } else if (moveCountdownRight <= 0){
                pointSelection++;
            }

            if(moveCountdownLeft > 0.09 && moveCountdownLeft < 0.10) {
                randomSwitch = true;
            } else if (moveCountdownRight > 0.09 && moveCountdownRight < 0.10){
                randomSwitch = true;
            }

            if(randomShotValue >= 1){
                ShootingOwl();
                randomShotValue = 0;
            }

            if(randomSwitch){
                randomShotValue = Random.Range(1, 2);
                randomSwitch = false;
            }

            if(pointSelection <= 0 || pointSelection >= 6){
                flyingAnimation = false;
            } else {
                flyingAnimation = true;
            }

            currentPoint = points[pointSelection];
        }
    }

    void ShootingOwl(){
        GameObject go = (GameObject)Instantiate(projectile, shotpoint.position, Quaternion.identity);
        go.GetComponent<OwlbotBenProjectile>().yspeed -= shotSpeed;
    }
}
