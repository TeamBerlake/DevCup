using UnityEngine;
using System.Collections;

public class HeartGroup : MonoBehaviour 
{
	void Update ()
	{
		if (transform.childCount == 0)
			Main.Instance.OpenNextLevel();
	}
}
