using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketDirectionWithAngle : MonoBehaviour {

    public float AngleToMove;
    public float SpeedOfRocket;

    private Rigidbody2D m_rigid2d;
	// Use this for initialization
	void Start () {
        m_rigid2d = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + AngleToMove);
        m_rigid2d.velocity = transform.right * SpeedOfRocket;

    }
}
