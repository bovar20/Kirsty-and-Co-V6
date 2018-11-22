using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirstyUIPortraitScript : MonoBehaviour {

    public bool kirstyhappyport;

    public bool kirstyokport;

    public bool kirstynotgoodport;

    public bool kirstysadport;

    public Animator anim;


	// Use this for initialization
	void Start () {
        kirstyhappyport = true;
	}
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("KirstyHappy", kirstyhappyport);
        anim.SetBool("KirstyOK", kirstyokport);
        anim.SetBool("KirstyNotGood", kirstynotgoodport);
        anim.SetBool("KirstySad", kirstysadport);
    }
}
