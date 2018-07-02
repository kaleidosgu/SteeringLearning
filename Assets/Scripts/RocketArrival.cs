using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketArrival : MonoBehaviour {

    public Transform transTarget;
    public float slowingRadius;
    public float max_velocity;
    public float max_speed;
    public float rotatingSpeed;

    private Rigidbody2D m_rigidBody;
    // Use this for initialization
    void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        Vector2 desired_velocity = transTarget.position - transform.position;
        float distance = desired_velocity.magnitude;
        
        if (distance < slowingRadius)
        {
            // Inside the slowing area
            // todo 这里的除法可以变成乘法
            desired_velocity = (desired_velocity).normalized * max_velocity * (distance / slowingRadius);
}
        else
        {
            // Outside the slowing area.
            desired_velocity = desired_velocity.normalized * max_velocity;
      }

        // Set the steering based on this
        Vector2 steering = desired_velocity - m_rigidBody.velocity;

        m_rigidBody.velocity = Vector2.ClampMagnitude(m_rigidBody.velocity + steering, max_speed);

        Vector2 point2Target = transTarget.position;

        point2Target.Normalize();

        float value = Vector3.Cross(point2Target, -transform.right).z;

        m_rigidBody.angularVelocity = rotatingSpeed * value;

    }
}
