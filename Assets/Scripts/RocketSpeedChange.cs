using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketSpeedChange : MonoBehaviour {
    public Vector2 SpeedToChange;
    public float TimeToChangeSpeed;
    public float SpeedRateToChange;

    private float m_fCurTimeToChangeSpeed;
    private HomingBehavior m_homingBehavior;
    private bool m_bChangeSpeed;
    private Rigidbody2D m_rigidbody;
    // Use this for initialization
    void Start ()
    {
        m_homingBehavior = GetComponent<HomingBehavior>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }
	
	// Update is called once per frame
	void Update () {
        if(m_bChangeSpeed == true)
        {
            if(m_fCurTimeToChangeSpeed >= TimeToChangeSpeed)
            {
                m_bChangeSpeed = false;
                m_homingBehavior.enabled = true;
            }
            m_fCurTimeToChangeSpeed += Time.deltaTime;
            m_rigidbody.velocity = Vector2.Lerp(m_rigidbody.velocity, SpeedToChange, Time.deltaTime * SpeedRateToChange);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(m_bChangeSpeed == false)
        {
            m_bChangeSpeed = true;
            m_fCurTimeToChangeSpeed = 0.0f;
            m_homingBehavior.enabled = false;
        }
    }
}
