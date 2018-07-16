using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketAnimationCurve : MonoBehaviour {

    public AnimationCurve AnimCurve;
    public float TimesToBiggerX;
    public float TimesToBiggerY;
    // Use this for initialization
    void Start () {
        AnimCurve.postWrapMode = WrapMode.Loop;

    }
	
	// Update is called once per frame
	void Update () {
		
        Vector3 vec = new Vector3(Time.time * TimesToBiggerX, AnimCurve.Evaluate(Time.time) * TimesToBiggerY, 0);
        transform.position = vec;

    }
}
