using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrouBolt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider coll)
    {
        //Debug.Log(coll.gameObject.tag);
        if(coll.gameObject.tag == "Bolt")
        {
            Destroy(coll.gameObject);
        }
    }
}
