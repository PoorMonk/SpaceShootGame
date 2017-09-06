using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestrouBolt : MonoBehaviour {

    private enemyController m_enemyController;
	// Use this for initialization
	void Start () {
        
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider coll)
    {
        //Debug.Log(coll.gameObject.tag);
        if(coll.gameObject.tag == "Bolt" || coll.gameObject.tag == "enemyBullet")
        {
            Destroy(coll.gameObject);
        }
        else if(coll.gameObject.tag == "enemy")
        {
            GameObject go = GameObject.Find("enemy");
            if (go != null)
            {
                m_enemyController = go.GetComponent<enemyController>();
                m_enemyController.SetEnemyStatus(false);
            }
            
            Destroy(coll.gameObject);
        }
    }
}
