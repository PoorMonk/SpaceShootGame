using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetRotation : MonoBehaviour {

    private Rigidbody m_Rigidbody;
    public float m_RotationAngle = 5;

	// Use this for initialization
	void Start () {
        m_Rigidbody = gameObject.GetComponent<Rigidbody>();
        m_Rigidbody.angularVelocity = Random.insideUnitSphere * m_RotationAngle;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
