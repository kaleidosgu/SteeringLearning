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
    public List<CircleCollider2D> LstCircleCollider;

    private Rigidbody2D m_rigid2d;
	// Use this for initialization
	void Start () {
		m_rigid2d = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		//有三条线。上中下，选择半径最近的值，对它进行回避。
		m_rigid2d.velocity = transform.right * SpeedOfRocket;

        foreach(CircleCollider2D colliderCheck in LstCircleCollider)
        {
            float fTransDistance = 0;
            float fUpDistance = 0;
            float fDownDistance = 0;
            bool bCollision = _isColliderCollision(transform, out fTransDistance,colliderCheck);
            bool bCollisionUp = _isColliderCollision(TransUp, out fUpDistance, colliderCheck);
            bool bCollisionDown = _isColliderCollision(TransDown, out fDownDistance, colliderCheck);

            bool bTrans = false;
            bool bTransUp = false;
            bool bTransDown = false;
            if (fTransDistance < fUpDistance)
            {
                if (fTransDistance < fDownDistance)
                {
                    bTrans = true;
                }
                else
                {
                    bTransDown = true;
                }
            }
            else if (fUpDistance <= fTransDistance)
            {
                if (fUpDistance < fDownDistance)
                {
                    bTransUp = true;
                }
                else
                {
                    bTransDown = true;
                }
            }
            bool bCheck = false;
            if (bTrans == true && bCollision == true)
            {
                _rotateRocket(transform);
                bCheck = true;
            }
            if (bTransUp == true && bCollisionUp == true)
            {
                _rotateRocket(transform);
                bCheck |= true;
            }
            if (bTransDown == true && bCollisionDown == true)
            {
                _rotateRocket(transform);
                bCheck |= true;
            }
            if( bCheck == true )
            {
                break;
            }
        }
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
        foreach( CircleCollider2D colliderCheck in LstCircleCollider)
        {
            bool bRes = _GizemosTransformProcess(transform, colliderCheck);
            if( bRes == true )
            {
                break;
            }
            bRes = _GizemosTransformProcess(TransDown, colliderCheck);
            if (bRes == true)
            {
                break;
            }
            bRes = _GizemosTransformProcess(TransUp, colliderCheck);
            if (bRes == true)
            {
                break;
            }
        }
	}

	private bool _GizemosTransformProcess(Transform transGizmos,CircleCollider2D colliderCheck)
	{
        bool bRes = false;
		Vector3 vecEndPos = _getTransformEndPos(transGizmos);
		float fDistance = 0;
		if( _isColliderCollision(transGizmos,out fDistance, colliderCheck) )
		{
			Gizmos.color = Color.red;
            bRes = true;
		}
		else
		{
			Gizmos.color = Color.blue;	
		}
		Gizmos.DrawLine(transGizmos.position,vecEndPos) ;
        return bRes;
	}

	private Vector3 _getTransformEndPos(Transform trans)
	{
		Vector3 vecEndPos = trans.right * DistanceOfCheck + trans.position;
		return vecEndPos;
	}

	private bool _isColliderCollision(Transform transCheck, out float outDistance,CircleCollider2D colliderCheck)
	{
		bool bRes = false;
		Vector3 vecCollider = colliderCheck.transform.position;
		Vector3 vecEndPos = transform.right * DistanceOfCheck + transCheck.position;
		float fDistance = Mathf.Sqrt((vecCollider.x - vecEndPos.x) * (vecCollider.x - vecEndPos.x) + (vecCollider.y - vecEndPos.y) * (vecCollider.y - vecEndPos.y));

        //当transform扩大scale的时候，collider会根据x，y中大的值进行放大。
        float fScale = colliderCheck.transform.localScale.x > colliderCheck.transform.localScale.y ? colliderCheck.transform.localScale.x : colliderCheck.transform.localScale.y;

        if ( fDistance < colliderCheck.radius * fScale)
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
