using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAroundTarget : MonoBehaviour
{

    public List<Transform> LstTargets;
    private int m_nCurrentIndex;
    private int m_nCountsTarget;
    private HomingBehavior m_homingBehavior;
    private Collider2D m_lastCollider;
    // Use this for initialization
    void Start()
    {
        m_nCountsTarget = LstTargets.Count;
        m_homingBehavior = GetComponent<HomingBehavior>();
        if (m_nCurrentIndex >= 0 && m_nCurrentIndex < m_nCountsTarget)
        {
            m_homingBehavior.ChangeNextTarget(LstTargets[m_nCurrentIndex]);
            m_lastCollider = LstTargets[m_nCurrentIndex].GetComponent<Collider2D>();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "PlannetColliderTag")
        {
            if(m_lastCollider != null)
            {
                m_lastCollider = collision;
                if( m_nCurrentIndex < m_nCountsTarget - 1)
                {
                    m_nCurrentIndex++;
                }
                else
                {
                    m_nCurrentIndex = 0;
                }
                m_homingBehavior.ChangeNextTarget(LstTargets[m_nCurrentIndex]);
            }
        }
    }
}
