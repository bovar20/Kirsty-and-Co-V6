using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirstyCustomLegs : MonoBehaviour {

    public int armsIndex;

    public GameObject TopLegLeft;
    public GameObject TopLegRight;
    public GameObject BottomLegLeft;
    public GameObject BottomLegRight;
    public GameObject BottomFootLeft;
    public GameObject BottomFootRight;


    // Update is called once per frame
    void Update()
    {
        TopLegLeft.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        TopLegRight.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomLegLeft.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomLegRight.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomFootLeft.GetComponent<KirstyCharacterApperence>().index = armsIndex;
        BottomFootRight.GetComponent<KirstyCharacterApperence>().index = armsIndex;
    }
}
