using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSteeringBehavior : MonoBehaviour {

    public float turnSpeed;
    public Transform TransTarget;
    public float MaxVelocity;
    public float MaxForce;

    private Rigidbody2D m_rigidBody;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 vecNew = (TransTarget.position - transform.position);
        Vector2 desired_velocity = vecNew.normalized * MaxVelocity;
        Vector2 vecSteering = desired_velocity - m_rigidBody.velocity;

        vecSteering = Vector2.ClampMagnitude(vecSteering, MaxForce);

        m_rigidBody.velocity = Vector2.ClampMagnitude(m_rigidBody.velocity + vecSteering, MaxVelocity);

        lookAtDirection(m_rigidBody.velocity);
    }
    public void lookAtDirection(Vector2 direction)
    {
        direction.Normalize();

        // If we have a non-zero direction then look towards that direciton otherwise do nothing
        if (direction.sqrMagnitude > 0.001f)
        {
            float toRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
            float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);

            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
}
