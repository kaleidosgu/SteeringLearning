using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimationCurveRotate : MonoBehaviour {

    public Transform transTarget;
    public AnimationCurve AnimCurve;
    public float TimesToBiggerX;
    public float TimesToBiggerY;
    
    private Vector3 m_vecLastLocalPos;
    private Vector3 m_vecLastAdd;
    // Use this for initialization
    void Start () {
        AnimCurve.postWrapMode = WrapMode.Loop;

        m_vecLastLocalPos = transform.localPosition;
        m_vecLastAdd = new Vector3();
    }
	
	// Update is called once per frame
	void Update () {
		
        Vector3 _vecAniCurve = new Vector3(Time.time * TimesToBiggerX, AnimCurve.Evaluate(Time.time) * TimesToBiggerY, 0);

        Vector3 vecNewPos = m_vecLastLocalPos + _vecAniCurve - m_vecLastAdd;
        Vector3 diff = vecNewPos - transform.localPosition;
        float angle = Mathf.Atan2(diff.y, diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*angle);

        transform.localPosition = vecNewPos;
        m_vecLastAdd = _vecAniCurve;
        m_vecLastLocalPos = transform.localPosition;
    }
}
