using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketChangeRotationSpeed : MonoBehaviour {

    public float ChangeRotatingSpeed;
    public float TimeToChangeRotating;
    public Transform TargetCenterTrans;
    public float turnSpeed;

    private float m_fCurrentTimeToChange;
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
                m_homingBehavior.enabled = true;
            }

            Vector2 vecSelf = new Vector2(transform.position.x, transform.position.y);
            Vector2 vecTarget = new Vector2(TargetCenterTrans.position.x, TargetCenterTrans.position.y);
            Vector2 direction = vecTarget - vecSelf;
            float toRotation = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);

            float rotation = Mathf.LerpAngle(transform.rotation.eulerAngles.z, toRotation, Time.deltaTime * turnSpeed);

            transform.rotation = Quaternion.Euler(0, 0, rotation);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        m_homingBehavior.enabled = false;
        m_bChangeRotating = true;
        m_fCurrentTimeToChange = 0.0f;


    }
}
