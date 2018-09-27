using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirstyCustomHead : MonoBehaviour {

    public int headIndex;

    public GameObject BowRight;
    public GameObject BowLeft;
    public GameObject Hairbandshown;
    public GameObject HeadbandFrame1;
    public GameObject HeadbandFrame2;
    public GameObject HeadbandFrame3;
    public GameObject HeadbandFrame4;
    public GameObject Head;
    public GameObject Face;
    public GameObject Ears;


    // Update is called once per frame
    void Update()
    {
        BowRight.GetComponent<KirstyCharacterApperence>().index = headIndex;
        BowLeft.GetComponent<KirstyCharacterApperence>().index = headIndex;
        Head.GetComponent<KirstyCharacterApperence>().index = headIndex;
        HeadbandFrame1.GetComponent<KirstyCharacterApperence>().index = headIndex;
        HeadbandFrame2.GetComponent<KirstyCharacterApperence>().index = headIndex;
        HeadbandFrame3.GetComponent<KirstyCharacterApperence>().index = headIndex;
        HeadbandFrame4.GetComponent<KirstyCharacterApperence>().index = headIndex;


        if (headIndex == 1 || headIndex == 5)
        {
            Hairbandshown.SetActive(true);
        } else if (headIndex == 6) {
            Hairbandshown.SetActive(true);
            Face.SetActive(false);
            Ears.SetActive(false);
        } else {
            Hairbandshown.SetActive(false);
            Face.SetActive(true);
            Ears.SetActive(true);
        }
    }
}
