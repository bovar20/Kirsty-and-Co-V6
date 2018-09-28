using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KirstyCustomBody : MonoBehaviour {

    public int armsIndex;

    public GameObject Body;

    // Update is called once per frame
    void Update()
    {
        Body.GetComponent<KirstyCharacterApperence>().index = armsIndex;
    }
}
