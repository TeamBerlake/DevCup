using UnityEngine;
using System.Collections;

public class LongRect : Shape
{
	
	#region variables
	
	private	const	float	m_speed		= 4.0f;
	
	#endregion // variables
	
	#region Monobehavior Functions
	
	// Update is called once per frame
	protected override void _Update () 
	{
		//apply state
		if (!Main.Instance.IsPlaying)
			return;

		if (m_canBeMoved)
		{
			Vector3 move = this.gameObject.transform.position;
			move.x += m_speed*Time.deltaTime;
			if (move.x > Camera.main.ViewportToWorldPoint(new Vector2(1.5f, 0.0f)).x)
				move.x = 0;
			this.gameObject.transform.position = move;
		}
	}
	
	#endregion // Monobehavior Functions
}

