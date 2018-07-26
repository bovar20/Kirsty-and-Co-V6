using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowBound : MonoBehaviour {

	//what we are following
	public Transform target;

	//zero's out the velocity
	Vector3 velocity = Vector3.zero;

	// time to follow target
	public float smoothTime = 15f;

	//enable and set the maximum Y value
	public bool YMaxEnabled = true;
	public float YMaxValue = 0;
	//enable and set the minimale Y value
	public bool YMinEnabled = true;
	public float YMinValue = 0;
	//enable and set the maximum X value
	public bool XMaxEnabled = true;
	public float XMaxValue = 0;
	//enable and set the minimale X value
	public bool XMinEnabled = true;
	public float XMinValue = 0;





	void Update()
	{
		CameraMovement ();
	}


	void CameraMovement()
	{
		//Target Position
		Vector3 targetPos = target.position;



		//Vertical
		if (YMinEnabled && YMaxEnabled) {
			targetPos.y = Mathf.Clamp (target.position.y, YMinValue, YMaxValue);
		}  else if (YMinEnabled) {
			targetPos.y = Mathf.Clamp (target.position.y, YMinValue, target.position.y);
		}  else if (YMaxEnabled) {
			targetPos.y = Mathf.Clamp (target.position.y, target.position.y, YMaxValue);
		}

		//Horizontal
		if (XMinEnabled && XMaxEnabled) {
			targetPos.x = Mathf.Clamp (target.position.x, XMinValue, XMaxValue);
		}  else if (XMinEnabled) {
			targetPos.x = Mathf.Clamp (target.position.x, XMinValue, target.position.x);
		}  else if (XMaxEnabled) {
			targetPos.x = Mathf.Clamp (target.position.x, target.position.x, XMaxValue);
		}


		//align the camrea and the targets z position
		targetPos.z = transform.position.z;

		//using smooth damp we will gradually change the camera transform position to the target position based on the cameras transform
		// velocity and our smoothTime.
		transform.position = Vector3.SmoothDamp (transform.position, targetPos, ref velocity, smoothTime);

	}
}
