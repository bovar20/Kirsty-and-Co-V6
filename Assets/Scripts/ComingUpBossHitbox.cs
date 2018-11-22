using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComingUpBossHitbox : MonoBehaviour {

    public CameraFollowBound cambound;

    public GameObject Boundaryleft;

    private bool CameraFix = false;

    public LevelManager levelman;

	// Use this for initialization
	void Start () {
        cambound = FindObjectOfType<CameraFollowBound>();

        levelman = FindObjectOfType<LevelManager>();
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
                CameraFix = false;
                gameObject.SetActive(false);
            }
        }
	}

	void OnTriggerStay2D(Collider2D other){
        if (other.tag == "Kirsty_Player" && !CameraFix){
            Boundaryleft.SetActive(true);
            cambound.XMinValue = 570f;
            cambound.YMaxValue = -11.8f;
            if(levelman.RespawnSet){
                cambound.YMinValue = -11.8f;
            } else {
                cambound.YMinValue = -14f;
                CameraFix = true;
            }
        }
	}
}
