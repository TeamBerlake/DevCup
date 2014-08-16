using UnityEngine;
using System.Collections;

public class Circle : Shape {

	#region variables

	private	const	float	m_speed		= 10.0f;

	#endregion // variables

	#region Monobehavior Functions
	
	// Update is called once per frame
	protected override void _Update ()
	{
		if (m_canBeMoved)
		{
			Vector3 move = this.gameObject.transform.position;
			move.y -= m_speed*Time.deltaTime;
			if (move.y < Camera.main.ViewportToWorldPoint(new Vector2(0.0f, -.1f)).y)
			{
				move.y = Camera.main.ViewportToWorldPoint(new Vector2(0.0f, -.1f)).y;
			}
			this.gameObject.transform.position = move;
		}
	}

	#endregion // Monobehavior Functions
}
