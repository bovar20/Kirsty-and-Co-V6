using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour {


	//Be aware there might be a bug in this

	public Transform[] backgrounds; //Gives an array of backgrounds within the parallax

	private float[] parallaxScales; //The value of speed of the backgrounds; finding it by scale 

	public float smoothing; //Trys to keep up with the player

	private Transform cam; //Stay's with the cam

	private Vector2 previousCamPos; //Previous Camara position

	// Use this for initialization
	void Start () {
		cam = Camera.main.transform;

		previousCamPos = cam.position;

		parallaxScales = new float[backgrounds.Length];

		for (int i = 0; i < backgrounds.Length; i++) {
			parallaxScales [i] = backgrounds [i].position.z * 1;
		}
	}
	
	// Update is called once per frame
	void LateUpdate () {
		for (int i = 0; i < backgrounds.Length; i++) {
			float parallax = (previousCamPos.x - cam.position.x) * parallaxScales [i];

			float backgroundTargetPosX = backgrounds [i].position.x + parallax;

			Vector3 backgroundTargetPos = new Vector3 (backgroundTargetPosX, backgrounds [i].position.y, backgrounds [i].position.z);

			backgrounds [i].position = Vector3.Lerp (backgrounds [i].position, backgroundTargetPos, smoothing * Time.deltaTime);
		}

		previousCamPos = cam.position;
	}
	//Note: I am not enterily sure what this code means as I followed a tutorial but...
	// The amount of background that are put on in the Unity Engine and depending on the Z axis
	// The further one background is on the Z axis, the slower the last background will be.
}
