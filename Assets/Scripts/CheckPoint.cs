using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour {

	public LevelManager levelManager;
    private Animator anim;
    public bool isCheckpointGO;

	// Use this for initialization
	void Start () {
		levelManager = FindObjectOfType<LevelManager>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("isCheckpoint", isCheckpointGO);
			}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.name == "Kirsty") 
		{
			levelManager.currentCheckpoint = gameObject;
            isCheckpointGO = true;
		}
	}
}
