using UnityEngine;
using System.Collections;

public class Circle : Shape {

	#region variables
	
	private	const	float			m_speed				= 10.0f;
	private			MoveDirection	m_direction			= MoveDirection.DOWN;
	private 		bool			m_isObstructed		= false;

	#endregion // variables

	#region Scene References

	private			Collider		m_obsCol			= null;

	#endregion // Scene References

	#region Monobehavior Functions
	
	// Update is called once per frame
	protected override void _Update ()
	{
		if (!m_canBeMoved)
		{
			return;
		}

		if (m_isObstructed)
		{
			Transform otherT = m_obsCol.transform;
			if (collider == null)
				return;
			if ((transform.position.x + (collider.bounds.extents.x * 0.5f)) < (otherT.position.x - (m_obsCol.bounds.extents.x * 0.5f)))
			/*
			if (otherR.bounds.min.x > myR.bounds.max.x)
			{
				m_isObstructed = false;
				m_obsCol = null;
			}
			else if (otherR.bounds.max.x < myR.bounds.min.x)
			{
				m_isObstructed = false;
				m_obsCol = null;
			}
			*/
			Debug.Log ("stopped");
			return;
		}

		Vector3 move = this.gameObject.transform.position;
		move.y -= m_speed*Time.deltaTime;
		if (move.y < Camera.main.ViewportToWorldPoint(new Vector2(0.0f, -.1f)).y)
		{
			move.y = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, -.1f)).y;
		}
		this.gameObject.transform.position = move;
	}

	protected override void _OnTriggerEnter (Collider col)
	{
		Debug.Log("hit");

		switch (m_direction)
		{
		case MoveDirection.DOWN:
			// If this object is higher
			if (col.transform.position.x < transform.position.x)
			{
				Debug.Log("obstructed");
				m_isObstructed = true;
				m_obsCol = col;
			}
			break;
		default:
			break;
		}
	}

	protected override void _OnTriggerExit ()
	{

	}

	#endregion // Monobehavior Functions
}
