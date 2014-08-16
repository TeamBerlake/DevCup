using UnityEngine;
using System.Collections;

public class Triangle : Shape {

	#region variables
	
	private	const	float	m_speed			= 64.0f;
	private			Vector3	m_center		= Vector3.zero;

	#endregion // variables
	
	#region Monobehavior Functions
	
	private void Start ()
	{
		m_center = this.transform.position + Vector3.one;
	}
	
	// Update is called once per frame
	protected override void _Update () 
	{
		//apply state
		if (m_canBeMoved)
		{
			this.transform.RotateAround(m_center, Vector3.forward, (m_speed*Time.deltaTime));
		}
	}
	
	#endregion // Monobehavior Functions
}
