using UnityEngine;
using System.Collections;

public class Circle : Shape {

	#region variables

	private	const	float	m_speed		= 2.0f;

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
			move.y -= m_speed*Time.deltaTime;
			if (move.y < Camera.main.ViewportToWorldPoint(new Vector2(0.0f, -.5f)).y)
			{
				move.y = 0;
			}
			this.gameObject.transform.position = move;
		}
	}

	#endregion // Monobehavior Functions
}
