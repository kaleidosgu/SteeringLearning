using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionAvoidance : MonoBehaviour {

	public float SpeedOfRocket;
	public float DistanceOfCheck;

	private Rigidbody2D m_rigid2d;
	// Use this for initialization
	void Start () {
		m_rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		m_rigid2d.velocity = transform.right * SpeedOfRocket;
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawLine(transform.position,transform.right * DistanceOfCheck + transform.position) ;
	}
}
