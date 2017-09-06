using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bolt : MonoBehaviour {

    public float m_speed = 20.0f;
    private Rigidbody m_Rigidbody;
    private Transform m_Transform;

    private Vector3 m_direction;

	// Use this for initialization
	void Start () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Transform = gameObject.GetComponent<Transform>();
        m_direction = m_Transform.forward;
	}
	
	// Update is called once per frame
	void Update () {
        //
	}

    void FixedUpdate()
    {
        m_Rigidbody.velocity = m_direction * m_speed;
        if(m_Rigidbody.position.z < -5.0f)
        {
            Destroy(gameObject);
        }
        //m_Transform.localToWorldMatrix();
    }
}
