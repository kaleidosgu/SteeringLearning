using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlannetMovement : MonoBehaviour {

    public Camera MainCamera;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Vector2 vecMouse = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        transform.position = new Vector3(vecMouse.x, vecMouse.y, transform.position.z);

    }
}
