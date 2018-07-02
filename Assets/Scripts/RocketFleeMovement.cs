using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketFleeMovement : MonoBehaviour {

    public float Max_Velocity;
    public Transform TransTarget;
    private Rigidbody2D m_rigidbody;
	// Use this for initialization
	void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 desired_velocity = Vector3.Normalize( transform.position - TransTarget.position) * Max_Velocity;
        Vector2 steering = desired_velocity - m_rigidbody.velocity;

        m_rigidbody.velocity = Vector2.ClampMagnitude(m_rigidbody.velocity + steering, Max_Velocity);

    }
}
