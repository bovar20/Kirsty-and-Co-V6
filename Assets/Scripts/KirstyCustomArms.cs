using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirstyCustomArms : MonoBehaviour {

    public int armsIndex;

    public GameObject TopArmLeft;
    public GameObject TopArmRight;
    public GameObject BottomArmLeft;
    public GameObject BottomArmRight;
    public GameObject BottomHandLeft;
    public GameObject BottomHandRight;
    public GameObject GunArmFrame1;
    public GameObject GunArmFrame2;
    public GameObject GunArmFrame3;


    // Update is called once per frame
    void Update()
    {
        TopArmLeft.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        TopArmRight.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomArmLeft.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomArmRight.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomHandLeft.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomHandRight.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        GunArmFrame1.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        GunArmFrame2.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        GunArmFrame3.GetComponent<KirstyCharacterApperence>().index = armsIndex;
    }
}
