using UnityEngine;
using System.Collections;

public class Movable : MonoBehaviour
{
	void OnMouseDrag ()
	{
		if (!Main.Instance.IsPlaying)
			transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition + new Vector3(0,0,100));
	}
}
