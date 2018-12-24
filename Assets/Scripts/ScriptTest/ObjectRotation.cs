using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotation : MonoBehaviour {

    public Transform objectTransform;
    public float turnSpeed;
    public float movementSpeed;
    private Rigidbody2D rigidbody;
	// Use this for initialization
	void Start () {
        rigidbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        Vector2 vec = objectTransform.position - transform.position;

        float fAngle = Mathf.Atan2(vec.y,vec.x) * Mathf.Rad2Deg;

        float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, fAngle, Time.deltaTime * turnSpeed);

        transform.rotation = Quaternion.Euler(0, 0, fAngle);

        vec.Normalize();
        rigidbody.velocity = vec * movementSpeed;
    }
}
