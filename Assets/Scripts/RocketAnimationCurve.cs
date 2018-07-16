using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimationCurve : MonoBehaviour {

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
		
        Vector3 vec = new Vector3(Time.time * TimesToBiggerX, AnimCurve.Evaluate(Time.time) * TimesToBiggerY, 0);
        transform.localPosition = m_vecLastLocalPos + vec - m_vecLastAdd;
        m_vecLastAdd = vec;
        m_vecLastLocalPos = transform.localPosition;
    }
}
