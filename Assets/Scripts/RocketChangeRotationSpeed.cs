using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketChangeRotationSpeed : MonoBehaviour {

    public float ChangeRotatingSpeed;
    public float TimeToChangeRotating;

    private float m_fCurrentTimeToChange;
    private Collider2D m_lastCollider;
    private HomingBehavior m_homingBehavior;

    private bool m_bChangeRotating;
    // Use this for initialization
    void Start () {

        m_homingBehavior = GetComponent<HomingBehavior>();
    }
	
	// Update is called once per frame
	void Update () {
        if(m_bChangeRotating == true)
        {
            m_fCurrentTimeToChange += Time.deltaTime;
            if(m_fCurrentTimeToChange >= TimeToChangeRotating)
            {
                m_bChangeRotating = false;
                m_homingBehavior.ResetRotatingSpeed();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_homingBehavior.ChangeRotationSpeed(ChangeRotatingSpeed);
        m_bChangeRotating = true;
        m_fCurrentTimeToChange = 0.0f;
    }
}
