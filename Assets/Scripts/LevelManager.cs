using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;

	private CharacterController2D player;

	//public GameObject deathParticle;
	//public GameObject respawnParticle;

	public float respawnDelay;

	private float gravityStore;

    public int pointPenaltyOnDeath;

	public HealthManager healthManager;

    public DrillmoleyRobAI DrillmoleyRob;

    public GameObject DeadGameObject;

    public bool enemyRespawn = false;

    public bool DrillmoleyRobRespawnPos = false;

    public bool OwlBotRespawn = false;

    public bool BullSteveAreaRespawn = false;

    public PlayerMovement playerMove;


	//public TimeManager timeManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<CharacterController2D>();

		healthManager = FindObjectOfType<HealthManager> ();

        playerMove = FindObjectOfType<PlayerMovement>();

		//timeManager = FindObjectOfType<TimeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void RespawnPlayer()
	{
		StartCoroutine ("DeadAnimationPlay");
	}


    public IEnumerator DeadAnimationPlay()
    {
        player.gameObject.SetActive(false);
        playerMove.player_hit = false;
        gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
        Instantiate(DeadGameObject, player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(2);
        StartCoroutine("RespawnPlayerCo");
    }

	public IEnumerator RespawnPlayerCo()
	{
		Debug.Log ("Player Respawn");
        //Instantiate (deathParticle, player.transform.position, player.transform.rotation);

        yield return new WaitForSeconds(respawnDelay);
        DrillmoleyRobRespawnPos = true;
        OwlBotRespawn = true;
        BullSteveAreaRespawn = true;
        if (!player.m_FacingRight){
            player.Flip();
        }
        enemyRespawn = true;
		player.GetComponent<Rigidbody2D> ().gravityScale = gravityStore;
        player.transform.position = currentCheckpoint.transform.position;
        player.enabled = true;
        player.shotDelay = false;
        player.shooting = false;
        player.gameObject.SetActive (true);
        healthManager.FullHealth ();
		healthManager.isDead = false;
		ScoreManager.score = 0;
        //Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
        //timeManager.ResetTime ();
    }
}
