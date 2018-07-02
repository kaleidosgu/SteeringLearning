using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketMovement : MonoBehaviour {

    public Camera MainCamera;
    public float MaxVelocity;
    public float MaxForce;
    public float rotatingSpeed;

    private Rigidbody2D m_rigidbody;


    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector3 vecMousePos = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        Vector2 vecNew = (vecMousePos - transform.position);
        Vector2 desired_velocity = vecNew.normalized * MaxVelocity;
        Vector2 vecSteering = desired_velocity - m_rigidbody.velocity;

        vecSteering = Vector2.ClampMagnitude(vecSteering, MaxForce);

        m_rigidbody.velocity = Vector2.ClampMagnitude(m_rigidbody.velocity + vecSteering, MaxVelocity);

        Vector2 point2Target = vecNew;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, -transform.right).z;

        m_rigidbody.angularVelocity = rotatingSpeed * value;

    }
}
