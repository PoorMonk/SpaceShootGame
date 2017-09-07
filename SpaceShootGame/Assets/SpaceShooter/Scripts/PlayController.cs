using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin = -6.5f;
    public float xMax = 6.5f;
    public float zMin = -4.0f;
    public float zMax = 13.5f;
}

public class PlayController : MonoBehaviour {

    public float m_speed = 10.0f;
    public Boundary m_Boundary;

    private Rigidbody m_Rigidbody;
    private Transform m_Transform;
    private AudioSource m_AudioSource;
    private GameControllor m_GameController;
    private UIController m_UIController;

    public GameObject m_explosionPlayer;
    public GameObject m_bolt;

    void Start()
    {
        Debug.Log("Start");
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_AudioSource = gameObject.GetComponent<AudioSource>();
        m_GameController = GameObject.FindWithTag("GameController").GetComponent<GameControllor>();
        m_UIController = GameObject.Find("UI").GetComponent<UIController>();
    }

    //public float speed = 10.0F;
    //public float rotationSpeed = 100.0F;
    void Update()
    {
        if(/*Input.GetKeyDown(KeyCode.Space)*/Input.GetMouseButtonDown(0))
        {
            Vector3 TLauchPos = GameObject.Find("LauchPos").gameObject.GetComponent<Transform>().position;
            //Debug.Log("bullet:" + TLauchPos.position.x + "," + TLauchPos.position.y + "," + TLauchPos.position.z);
            Instantiate(m_bolt, TLauchPos, Quaternion.identity);
            m_AudioSource.Play();
        }
    }

    
    
    void FixedUpdate()
    {
        float VerticalMove = Input.GetAxis("Vertical");
        float HorizontalMove = Input.GetAxis("Horizontal");

        //Debug.Log("Vertical:" + VerticalMove);
        //Debug.Log("Hirozontal:" + HorizontalMove);
        Vector3 vmove = new Vector3(HorizontalMove, 0f, VerticalMove);
        
        m_Rigidbody.velocity = vmove * m_speed;
        m_Rigidbody.position = new Vector3(
            Mathf.Clamp(m_Rigidbody.position.x, m_Boundary.xMin, m_Boundary.xMax),
            0,
            Mathf.Clamp(m_Rigidbody.position.z, m_Boundary.zMin, m_Boundary.zMax));
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "enemyBullet")
        {
            Destroy(gameObject);
            Destroy(coll.gameObject);
            Instantiate(m_explosionPlayer, transform.position, Quaternion.identity);
            m_GameController.GameOver();
            m_UIController.ChangeGameStatus(UIController.GAMESTATUS.GAMEEND);
        }
    }
}
