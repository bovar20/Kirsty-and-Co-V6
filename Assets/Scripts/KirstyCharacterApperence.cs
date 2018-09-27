using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirstyCharacterApperence : MonoBehaviour {


    public SpriteRenderer part;
    public Sprite[] options;
    public int index;

	// Update is called once per frame
	void Update () {

        for (int i = 0; i < options.Length; i++) {

            if(i == index){
                part.sprite = options[i]; 
            }
        }
	}

    public void Swap(){

        if(index < options.Length - 1){
            index++;
        } else {
            index = 0;
        }
            
    }
}
