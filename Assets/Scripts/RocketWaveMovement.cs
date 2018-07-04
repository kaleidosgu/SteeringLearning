using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketWaveMovement : MonoBehaviour {

	public float SpeedOfRocket;
    public float TurnSpeed;
    public float TimeToChangeDirection;

    private float m_fCurrentTimeChangeDirection;
    private Rigidbody2D m_rigidbody;
    private float m_fCurrentTurnSpeed;
    private bool m_bRightDirection;
    void Start () {
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_fCurrentTurnSpeed = TurnSpeed;
    }
	
	// Update is called once per frame
	void Update () {
		m_rigidbody.velocity = transform.right * SpeedOfRocket;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, transform.rotation.eulerAngles.z + m_fCurrentTurnSpeed);

        m_fCurrentTimeChangeDirection += Time.deltaTime;
        if( m_fCurrentTimeChangeDirection >= TimeToChangeDirection )
        {
            m_fCurrentTimeChangeDirection -= TimeToChangeDirection;
            if( m_fCurrentTurnSpeed < 0 )
            {
                m_fCurrentTurnSpeed = TurnSpeed;
            }
            else
            {
                m_fCurrentTurnSpeed = -TurnSpeed;
            }
        }
    }

}
