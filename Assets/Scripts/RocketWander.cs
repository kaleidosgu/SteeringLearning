using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWander : MonoBehaviour {

    public Transform transTarget;
    public float MaxVelocity;
    public float MaxForce;
    public float max_speed;
    public bool Displacement;
    public float CIRCLE_DISTANCE;
    public float ANGLE_CHANGE;
    public float rotatingSpeed;
    private Rigidbody2D m_rigidbody2d;
    private float wanderAngle;
    // Use this for initialization
    void Start ()
    {
        wanderAngle = 30;
        m_rigidbody2d = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if(Displacement == false)
        {
            Vector2 steering = wander();
            steering = Vector2.ClampMagnitude(steering, MaxForce);
            steering = steering / m_rigidbody2d.mass;
            m_rigidbody2d.velocity = Vector2.ClampMagnitude(m_rigidbody2d.velocity + steering, max_speed);
            //position = position + velocity
        }
        else
        {
            Vector2 circleCenter = new Vector2(); ;
            circleCenter = new Vector2(m_rigidbody2d.velocity.x, m_rigidbody2d.velocity.y);
            circleCenter = circleCenter.normalized;
            circleCenter = circleCenter * (CIRCLE_DISTANCE);

            Vector2 displacement = new Vector2();
            displacement = new Vector2(0, -1);
            displacement = displacement * CIRCLE_DISTANCE;


            displacement = setAngle(displacement, wanderAngle);

            float fDiff = Random.Range(0.0f, 1.0f) * ANGLE_CHANGE - ANGLE_CHANGE * 0.5f;
            Debug.Log(fDiff);
            wanderAngle += fDiff;

            Vector2 wanderForce;
            wanderForce = circleCenter + (displacement);

            seek();
            Vector2 steering = wanderForce;
            steering = Vector2.ClampMagnitude(steering, MaxForce);
            steering = steering / m_rigidbody2d.mass;
            m_rigidbody2d.velocity = Vector2.ClampMagnitude(m_rigidbody2d.velocity + steering, max_speed);

            Vector2 point2Target = transTarget.position;
            point2Target.Normalize();
            float value = Vector3.Cross(point2Target, -transform.right).z;
            m_rigidbody2d.angularVelocity = rotatingSpeed * value;
        }
    }

    public void seek()
    {
        Vector2 vecNew = (transTarget.position - transform.position);
        Vector2 desired_velocity = vecNew.normalized * MaxVelocity;
        Vector2 vecSteering = desired_velocity - m_rigidbody2d.velocity;

        vecSteering = Vector2.ClampMagnitude(vecSteering, MaxForce);

        m_rigidbody2d.velocity = Vector2.ClampMagnitude(m_rigidbody2d.velocity + vecSteering, MaxVelocity);
    }
    public Vector2 setAngle(Vector2 vector, float value)
    {
        float fDistance = vector.magnitude;
        vector.x = Mathf.Cos(value) * fDistance;
        vector.y = Mathf.Sin(value) * fDistance;
        return vector;
    }

private Vector2 wander()
    {
        Vector2 vecNew = (transTarget.position - transform.position);
        Vector2 desired_velocity = vecNew.normalized * MaxVelocity;
        Vector2 vecSteering = desired_velocity - m_rigidbody2d.velocity;

        vecSteering = Vector2.ClampMagnitude(vecSteering, MaxForce);

        m_rigidbody2d.velocity = Vector2.ClampMagnitude(m_rigidbody2d.velocity + vecSteering, MaxVelocity);

        return vecSteering;
    }
}
