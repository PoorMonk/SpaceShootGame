using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyPlanet : MonoBehaviour {

    public GameObject m_explosionPlanet;
    public GameObject m_explosionPlayer;
    private GameControllor m_GameController;
    private UIController m_UIController;

    public int m_score;
    
	// Use this for initialization
	void Start () {
        Destroy(gameObject, 10.0f);
        m_GameController = GameObject.FindWithTag("GameController").GetComponent<GameControllor>();
        m_UIController = GameObject.Find("UI").GetComponent<UIController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter(Collider coll)
    {
        //Debug.Log("Enter...");
        //Debug.Log("name:" + coll.gameObject.name);
        if(coll.gameObject.tag != "Boundary")
        {
            if(coll.gameObject.tag != "Planet")
            {
                if (coll.gameObject.tag == "Player")
                {
                    Instantiate(m_explosionPlayer, transform.position, Quaternion.identity);
                    //Destroy(m_explosionPlayer, 2.0f);
                    //Debug.Log("Player");
                    m_UIController.ChangeGameStatus(UIController.GAMESTATUS.GAMEEND);
                    m_GameController.GameOver();
                }
                else
                {
                    Destroy(coll.gameObject);
                    Destroy(gameObject);
                    Instantiate(m_explosionPlanet, transform.position, Quaternion.identity);
                    m_UIController.AddScore(m_score);
                }
                
            }      
        }
    }

}
