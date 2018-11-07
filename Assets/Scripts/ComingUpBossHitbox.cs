using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingUpBossHitbox : MonoBehaviour {

    public CameraFollowBound cambound;

    public GameObject Boundaryleft;

    private bool CameraFix = false;

	// Use this for initialization
	void Start () {
        cambound = FindObjectOfType<CameraFollowBound>();
	}
	
	// Update is called once per frame
	void Update () {
        if(CameraFix){
            cambound.YMinValue += Time.deltaTime;

            if (cambound.YMinValue >= -11.8f)
            {
                cambound.YMinValue = -11.8f;
            }
            if(cambound.XMinValue >= 570f && cambound.YMinValue >= -11.8f){
                Destroy(gameObject);
            }
        }
	}

	void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "Kirsty_Player" && !CameraFix){
            Boundaryleft.SetActive(true);
            cambound.XMinValue = 570f;
            cambound.YMinValue = -14f;
            CameraFix = true;
        }
	}
}
