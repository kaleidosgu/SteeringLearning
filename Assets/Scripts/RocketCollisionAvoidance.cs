using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketCollisionAvoidance : MonoBehaviour {

	public float SpeedOfRocket;
	public float DistanceOfCheck;
	public CircleCollider2D ColliderTarget; 
	public float ForceOfAvoidance;
	public Transform TransDown; 
	public Transform TransUp; 

	private Rigidbody2D m_rigid2d;
	// Use this for initialization
	void Start () {
		m_rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//有三条线。上中下，选择半径最近的值，对它进行回避。
		m_rigid2d.velocity = transform.right * SpeedOfRocket;

		float fTransDistance = 0;
		float fUpDistance = 0;
		float fDownDistance = 0;
		bool bCollision = _isColliderCollision(transform,out fTransDistance);
		bool bCollisionUp = _isColliderCollision(TransUp,out fUpDistance);
		bool bCollisionDown = _isColliderCollision(TransDown,out fDownDistance);
		
		bool bTrans = false;
		bool bTransUp = false;
		bool bTransDown = false;
		if( fTransDistance < fUpDistance )
		{
			if( fTransDistance < fDownDistance )
			{
				bTrans = true;
			}
			else
			{
				bTransDown = true;
			}
		}
		else if( fUpDistance <= fTransDistance )
		{
			if( fUpDistance < fDownDistance )
			{
				bTransUp = true;
			}
			else
			{
				bTransDown = true;
			}
		}
		if( bTrans == true && bCollision == true )
		{
			_rotateRocket(transform);
		}
		if( bTransUp == true && bCollisionUp == true )
		{
			_rotateRocket(transform);
		}
		if( bTransDown == true && bCollisionDown == true )
		{
			_rotateRocket(transform);
		}
		// if( bCollision == true )
		// {
		// 	Vector3 vecEndPos = transform.right * DistanceOfCheck + TransDown.position;
		// 	Vector3 vecPos3 = vecEndPos - ColliderTarget.transform.position;
		// 	Vector2 vec2 = new Vector2( vecPos3.x,vecPos3.y );
		// 	Vector2 vecNom = vec2.normalized;
		// 	float fAng = Mathf.Atan2(vecNom.y,vecNom.x) * Mathf.Rad2Deg;
		// 	fAng = ForceOfAvoidance * fAng;
		// 	transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z + fAng);
		// }
	}

	private void _rotateRocket(Transform transRotate)
	{
		Vector3 vecEndPos = transform.right * DistanceOfCheck + transRotate.position;
		Vector3 vecPos3 = vecEndPos - ColliderTarget.transform.position;
		Vector2 vec2 = new Vector2( vecPos3.x,vecPos3.y );
		Vector2 vecNom = vec2.normalized;
		float fAng = Mathf.Atan2(vecNom.y,vecNom.x) * Mathf.Rad2Deg;
		fAng = ForceOfAvoidance * fAng;
		transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x,transform.rotation.eulerAngles.y,transform.rotation.eulerAngles.z + fAng);
	}

	void OnDrawGizmos()
	{
		_GizemosTransformProcess(transform);
		_GizemosTransformProcess(TransDown);
		_GizemosTransformProcess(TransUp);
	}

	private void _GizemosTransformProcess(Transform transGizmos)
	{
		Vector3 vecEndPos = _getTransformEndPos(transGizmos);
		float fDistance = 0;
		if( _isColliderCollision(transGizmos,out fDistance) )
		{
			Gizmos.color = Color.red;
		}
		else
		{
			Gizmos.color = Color.blue;	
		}
		Gizmos.DrawLine(transGizmos.position,vecEndPos) ;
	}

	private Vector3 _getTransformEndPos(Transform trans)
	{
		Vector3 vecEndPos = trans.right * DistanceOfCheck + trans.position;
		return vecEndPos;
	}

	private bool _isColliderCollision(Transform transCheck, out float outDistance)
	{
		bool bRes = false;
		Vector3 vecCollider = ColliderTarget.transform.position;
		Vector3 vecEndPos = transform.right * DistanceOfCheck + transCheck.position;
		float fDistance = Mathf.Sqrt((vecCollider.x - vecEndPos.x) * (vecCollider.x - vecEndPos.x) + (vecCollider.y - vecEndPos.y) * (vecCollider.y - vecEndPos.y));
		if( fDistance < ColliderTarget.radius )
		{
			bRes = true;
		}
		else
		{
			bRes = false;
		}
		outDistance = fDistance;
		return bRes;
	}
}
