using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBehaviorBase : MonoBehaviour {

    public Transform TargetTrans;
    public float turnSpeed;
    public bool LerpAngle;
    public float movementSpeed;

    private Rigidbody2D m_rigidBody;
	// Use this for initialization
	void Start () {
        m_rigidBody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        ChangeVelocity(transform.position, TargetTrans.position);
        RotateFrom2Pos(transform.position,TargetTrans.position);

    }

    public void RotateToDirection(Vector2 vecDir)
    {
        float fAngle = Mathf.Atan2(vecDir.y, vecDir.x) * Mathf.Rad2Deg;

        if(LerpAngle == true)
        {
            fAngle = Mathf.LerpAngle(transform.rotation.eulerAngles.z, fAngle, Time.deltaTime * turnSpeed);
        }

        transform.rotation = Quaternion.Euler(0, 0, fAngle);
    }
    public void RotateFrom2Pos(Vector3 vecStart, Vector3 vecEnd)
    {
        Vector2 vecDir = vecEnd - vecStart;
        RotateToDirection(vecDir);
    }

    public void ChangeVelocity(Vector3 vecPosStart, Vector3 vecPosEnd)
    {
        Vector2 vecDir = vecPosEnd - vecPosStart;

        vecDir.Normalize();
        vecDir = vecDir * movementSpeed;

        Vector2 acceleration = vecDir - new Vector2(m_rigidBody.velocity.x, m_rigidBody.velocity.y);

        m_rigidBody.velocity += acceleration * Time.deltaTime;
    }
}
