using UnityEngine;
using System.Collections;

public class Triangle : Shape {

	#region variables
	
	private	const	float		m_speed			= 64.0f;
	private			Vector3		m_pivot			= Vector3.zero;

	#endregion // variables

	#region Scene References

	[SerializeField]
	private			Transform	m_pivotTform	= null;

	#endregion // Scene References
	
	#region Monobehavior Functions
	
	// Update is called once per frame
	protected override void _Update () 
	{
		//apply state
		if (m_canBeMoved)
		{
			this.transform.RotateAround(m_pivotTform.position, Vector3.forward, (m_speed*Time.deltaTime));
		}
	}
	
	#endregion // Monobehavior Functions
}
