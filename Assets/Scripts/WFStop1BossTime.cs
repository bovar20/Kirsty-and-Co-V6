using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WFStop1BossTime : MonoBehaviour {

    public CameraFollowBound cambound;

    public GameObject Boundaryleft;

	// Use this for initialization
	void Start () {
        cambound = FindObjectOfType<CameraFollowBound>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Kirsty_Player")
        {
            Boundaryleft.SetActive(true);
            cambound.XMinValue = 604.5f;
            cambound.XMaxValue = 604.5f;
            Destroy(gameObject);
        }
    }
}
