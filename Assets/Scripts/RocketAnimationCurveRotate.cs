using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimationCurveRotate : MonoBehaviour {

    public Transform transTarget;
    public float TimesToBiggerX;
    public float TimesToBiggerY;

    public List<AnimationCurve> LstAnimCurve;

    public AnimationCurve m_AnimCurve;
    public float TimeToChangeCurve;
    private Vector3 m_vecLastLocalPos;
    private Vector3 m_vecLastAdd;
    private float m_fCurrentTime;
    private int m_nIndex;
    // Use this for initialization
    void Start () {
        _ChangeCurve();
        m_vecLastLocalPos = transform.localPosition;
        m_vecLastAdd = new Vector3();
    }

    private void _ChangeCurve()
    {
        m_AnimCurve = LstAnimCurve[m_nIndex];
        m_AnimCurve.postWrapMode = WrapMode.Loop;
        if(m_nIndex < LstAnimCurve.Count - 1)
        {
            m_nIndex++;
        }
        else
        {
            m_nIndex = 0;
        }
    }
	
	// Update is called once per frame
	void Update () {

        m_fCurrentTime += Time.deltaTime;
        if( m_fCurrentTime >= TimeToChangeCurve )
        {
            m_fCurrentTime -= TimeToChangeCurve;
            _ChangeCurve();
        }
        Vector3 _vecAniCurve = new Vector3(Time.time * TimesToBiggerX, m_AnimCurve.Evaluate(Time.time) * TimesToBiggerY, 0);

        Vector3 vecNewPos = m_vecLastLocalPos + _vecAniCurve - m_vecLastAdd;
        Vector3 diff = vecNewPos - transform.localPosition;
        float angle = Mathf.Atan2(diff.y, diff.x);
        transform.rotation = Quaternion.Euler(0f, 0f, Mathf.Rad2Deg*angle);

        transform.localPosition = vecNewPos;
        m_vecLastAdd = _vecAniCurve;
        m_vecLastLocalPos = transform.localPosition;
    }
}
