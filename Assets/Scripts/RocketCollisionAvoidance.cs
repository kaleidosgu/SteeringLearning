using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionAvoidance : MonoBehaviour {

	public float SpeedOfRocket;
	public float DistanceOfCheck;
	public CircleCollider2D ColliderTarget; 
	public float ForceOfAvoidance;

	private Rigidbody2D m_rigid2d;
	// Use this for initialization
	void Start () {
		m_rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		m_rigid2d.velocity = transform.right * SpeedOfRocket;

		bool bCollision = _isColliderCollision();
		
		if( bCollision == true )
		{
			Vector3 vecEndPos = transform.right * DistanceOfCheck + transform.position;
			Vector3 vecPos3 = vecEndPos - ColliderTarget.transform.position;
			Vector2 vec2 = new Vector2( vecPos3.x,vecPos3.y );
			Vector2 vecNom = vec2.normalized;
			float fAng = Mathf.Atan2(vecNom.y,vecNom.x);
			fAng = ForceOfAvoidance * fAng;
			transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z + fAng);
		}
	}

	void OnDrawGizmos()
	{
		Vector3 vecEndPos = transform.right * DistanceOfCheck + transform.position;

		if( _isColliderCollision() )
		{
			Gizmos.color = Color.red;
		}
		else
		{
			Gizmos.color = Color.blue;	
		}
		

		Gizmos.DrawLine(transform.position,vecEndPos) ;
	}

	private bool _isColliderCollision()
	{
		bool bRes = false;
		Vector3 vecCollider = ColliderTarget.transform.position;
		Vector3 vecEndPos = transform.right * DistanceOfCheck + transform.position;
		float fDistance = Mathf.Sqrt((vecCollider.x - vecEndPos.x) * (vecCollider.x - vecEndPos.x) + (vecCollider.y - vecEndPos.y) * (vecCollider.y - vecEndPos.y));
		if( fDistance < ColliderTarget.radius )
		{
			bRes = true;
		}
		else
		{
			bRes = false;
		}
		return bRes;
	}
}
