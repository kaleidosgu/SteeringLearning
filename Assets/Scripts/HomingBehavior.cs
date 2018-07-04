using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomingBehavior : MonoBehaviour {

    public Transform TargetTransform;
    public float rotatingSpeed;
    public float SpeedOfRocket;
    public float TimeForIgnoringTarget;
    public Vector2 RandomRange;
    public bool StartToIgnore;

    public float AngleOfTurn;

    public float RocketFixedTime;
    public float TimeForMeetPlayer;

    private Rigidbody2D m_rigidbody;
    private bool m_bActiveIgnore;
    private float m_fTimeForIgnoringTarget;

    private float m_flastAngleOfTurn;
    private bool m_bFixedTiming;
    private float m_fCurFixedTime;
    private int m_nCurrentIndex;
    //private float m_fLastTimeMetPlayer;
    //private bool m_bMeetPlayer;
    // Use this for initialization
    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {

        if(m_bActiveIgnore == true && StartToIgnore == true)
        {
            m_fTimeForIgnoringTarget += Time.deltaTime;
            if(m_fTimeForIgnoringTarget >= TimeForIgnoringTarget)
            {
                m_bFixedTiming = true;
                m_bActiveIgnore = false;
                m_fCurFixedTime = 0.0f;
            }
            m_rigidbody.velocity = transform.right * SpeedOfRocket;
        }
        else
        {
            if (m_bFixedTiming == true)
            {
                if (m_fCurFixedTime >= RocketFixedTime)
                {
                    m_bFixedTiming = false;
                }
                m_fCurFixedTime += Time.deltaTime;
                transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + m_flastAngleOfTurn);
                m_rigidbody.velocity = transform.right * SpeedOfRocket;
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
        // if( m_bMeetPlayer == true )
        // {
        //     if( Time.time - m_fLastTimeMetPlayer > TimeForMeetPlayer )
        //     {
        //         m_fLastTimeMetPlayer = Time.time;
        //         m_bActiveIgnore = true;
        //         m_fTimeForIgnoringTarget = 0.0f;
        //         m_bFixedTiming = false;
        //         m_bMeetPlayer = false;
        //     }
        // }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if( collision.gameObject.tag == "PlannetColliderTag")
        {
            //m_bFixedTiming = true;
            m_bActiveIgnore = true;
            //m_rigidbody.angularVelocity = Random.Range(RandomRange.x, RandomRange.y);
            m_fTimeForIgnoringTarget = 0.0f;
            
            if (m_nCurrentIndex == 0)
            {
                m_flastAngleOfTurn = AngleOfTurn;
                m_nCurrentIndex = 1;
            }
            else
            {
                m_flastAngleOfTurn = -AngleOfTurn;
                m_nCurrentIndex = 0;
            }
            //m_fLastTimeMetPlayer = Time.time;
            //m_bMeetPlayer = true;
        }
    }
}
