using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;

	private CharacterController2D player;

	public GameObject deathParticle;
	public GameObject respawnParticle;

	public float respawnDelay;

	private float gravityStore;

    public int pointPenaltyOnDeath;

	//public HealthManager healthManager;

	//public TimeManager timeManager;

	// Use this for initialization
	void Start () {
        player = FindObjectOfType<CharacterController2D>();

		//healthManager = FindObjectOfType<HealthManager> ();

		//timeManager = FindObjectOfType<TimeManager> ();
	}
	
	// Update is called once per frame
	void Update () {
	}


	public void RespawnPlayer()
	{
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo()
	{
		Debug.Log ("Player Respawn");
		player.enabled = false;
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		player.gameObject.SetActive (false);
		//gravityStore = player.GetComponent<Rigidbody2D>().gravityScale;
		//player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero;
		yield return new WaitForSeconds (respawnDelay);
		//player.GetComponent<Rigidbody2D> ().gravityScale = gravityStore;
		player.transform.position = currentCheckpoint.transform.position;
		player.enabled = true;                                                                                    
		player.gameObject.SetActive (true);
		//healthManager.FullHealth ();
		//healthManager.isDead = false;
		ScoreManager.score = 0;
		Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		//timeManager.ResetTime ();

	}
}
