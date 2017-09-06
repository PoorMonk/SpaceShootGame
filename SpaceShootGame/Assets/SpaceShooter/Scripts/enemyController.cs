using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour {

    public float m_speedBullet = 10.0f;
    public float m_speedEnemy = -5.0f;
    private float m_waitTime = 1.0f;

    public int m_score = 30;
    public GameObject m_explosionEnemy;
    public GameObject m_explosionPlayer;
    private GameControllor m_GameController;
    private UIController m_UIController;
    private Transform m_Transform;

    private Rigidbody m_Rigidbody;
    public GameObject m_bolt;

    private bool m_enemyAlive = true;
    private float m_startTime = 0.0f;
    private Vector3 m_direction;

    // Use this for initialization
    void Start () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Transform = gameObject.GetComponent<Transform>();
        m_GameController = GameObject.FindWithTag("GameController").GetComponent<GameControllor>();
        m_UIController = GameObject.Find("UI").GetComponent<UIController>();
        m_direction = m_Transform.forward;
        //Invoke("SendBullet", 2.0f);

        //StartCoroutine(SendBullet());
    }
	
	// Update is called once per frame
	void Update () {
        //m_Rigidbody.velocity = transform.position * m_speed;
        m_startTime += Time.deltaTime;
        if(m_waitTime <= m_startTime)
        {
            Vector3 sendPos = GameObject.Find("enemyBullet").GetComponent<Transform>().position;
            //Vector3 enemyPos = m_Transform.position;
            //Debug.Log("position: " + sendPos.x + "," + sendPos.y + "," + sendPos.z);
            //Debug.Log("enemyposition: " + enemyPos.x + "," + enemyPos.y + "," + enemyPos.z);
            Instantiate(m_bolt, sendPos, Quaternion.identity);
            m_startTime = 0.0f;
        }
	}

    void FixedUpdate()
    {
        m_Rigidbody.velocity = m_direction * m_speedEnemy;
        m_Rigidbody.position = new Vector3(m_Rigidbody.position.x, 0.0f, m_Rigidbody.position.z);
    }

    public void CreateBulletInterface()
    {
        StartCoroutine(SendBullet());
    }

    /// <summary>
    /// 发射子弹
    /// </summary>
    IEnumerator SendBullet()
    {
        while(m_enemyAlive)
        {
            Vector3 sendPos = GameObject.Find("enemyBullet").GetComponent<Transform>().position;
            //Debug.Log("position: " + sendPos.x + "," + sendPos.y + "," + sendPos.z);
            Instantiate(m_bolt, sendPos, Quaternion.identity);
            yield return new WaitForSeconds(m_waitTime);
        }
    }

    public void SetEnemyStatus(bool bFlag)
    {
        m_enemyAlive = bFlag;
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag != "Boundary")
        {
            if (coll.gameObject.tag != "Planet" && coll.gameObject.tag != "enemy")
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
                    Instantiate(m_explosionEnemy, transform.position, Quaternion.identity);
                    m_UIController.AddScore(m_score);
                    
                }
                m_enemyAlive = false;
            }
        }
    }
}
