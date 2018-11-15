using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrillmoleyHollyAfterDeath : MonoBehaviour {

    public GameObject Barrier;

    public CameraFollowBound cambound;

    public LevelManager levelman;

	// Use this for initialization
	void Start () {

        cambound = FindObjectOfType<CameraFollowBound>();

        levelman = FindObjectOfType<LevelManager>();

        StartCoroutine("AfterDeath");
	}
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator AfterDeath(){
        yield return new WaitForSeconds(4.8f);
        levelman.BarrierDown = true;
        Destroy(gameObject);
    }
}
