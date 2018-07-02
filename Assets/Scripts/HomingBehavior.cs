using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehavior : MonoBehaviour {

    public Transform TargetTransform;
    public float rotatingSpeed;
    public float SpeedOfRocket;
    public float TimeForIgnoringTarget;
    public Vector2 RandomRange;

    private Rigidbody2D m_rigidbody;
    private bool m_bActiveIgnore;
    private float m_fTimeForIgnoringTarget;
    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if(m_bActiveIgnore == true)
        {
            m_fTimeForIgnoringTarget += Time.deltaTime;
            if(m_fTimeForIgnoringTarget >= TimeForIgnoringTarget)
            {
                m_bActiveIgnore = false;
            }
        }
        else
        {
            Vector2 point2Target = (Vector2)transform.position - (Vector2)TargetTransform.transform.position;

            point2Target.Normalize();

            float value = Vector3.Cross(point2Target, transform.right).z;
            float fChangeValueCache = rotatingSpeed * value;
            m_rigidbody.angularVelocity = fChangeValueCache * Time.deltaTime;
            m_rigidbody.velocity = transform.right * SpeedOfRocket;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "PlannetColliderTag")
        {
            m_bActiveIgnore = true;
            m_rigidbody.angularVelocity = Random.Range(RandomRange.x, RandomRange.y);
            m_fTimeForIgnoringTarget = 0.0f;
        }
    }
}
