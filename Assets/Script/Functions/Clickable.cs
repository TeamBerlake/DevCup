using UnityEngine;
using System.Collections;

public class Clickable : MonoBehaviour
{
	void OnMouseUp ()
	{
		if (!Main.Instance.IsPlaying)
			BroadcastMessage("OnClick", SendMessageOptions.DontRequireReceiver);
	}
}
