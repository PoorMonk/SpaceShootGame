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

    public GameObject m_bolt;

    void Start()
    {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_AudioSource = gameObject.GetComponent<AudioSource>();
    }

    //public float speed = 10.0F;
    //public float rotationSpeed = 100.0F;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Transform TLauchPos = GameObject.Find("LauchPos").gameObject.GetComponent<Transform>();
            GameObject.Instantiate(m_bolt, TLauchPos.position, Quaternion.identity);
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

}
