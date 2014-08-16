using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour
{
	private Color ToColor (Shape.ShapeColor sc)
	{
		switch (sc)
		{
		case Shape.ShapeColor.Blue:
			return Color.blue;
		case Shape.ShapeColor.Green:
			return Color.green;
		case Shape.ShapeColor.Grey:
			return Color.gray;
		case Shape.ShapeColor.Purple:
			return new Color(1.0f,0.0f,1.0f,1.0f);
		case Shape.ShapeColor.Red:
			return Color.red;
		case Shape.ShapeColor.Orange:
			return new Color(1.0f,0.5f,0.0f,1.0f);
		default:
			return Color.white;
		}
	}

	[SerializeField]
	Shape.ShapeColor color = Shape.ShapeColor.Grey;
	public Shape.ShapeColor HeartColor
	{
		get {return color;}
		set 
		{
			color = value;
			foreach (Transform t in transform.GetComponentsInChildren<Transform>())
				t.renderer.material.color = ToColor(value);
		}
	}

	void OnTriggerEnter (Collider col)
	{
		if (!Main.Instance.IsPlaying)
			return;

		Shape shape = col.transform.parent.GetComponent<Shape>();
		if (shape == null)
			return;

		if (shape.CurrentColor == color)
			Destroy (gameObject);
	}

	void OnTriggerStay (Collider col)
	{
		if (!Main.Instance.IsPlaying)
			return;
		
		Shape shape = col.transform.parent.GetComponent<Shape>();
		if (shape == null)
			return;

		if (shape.CurrentColor == color)
			Destroy (gameObject);
	}
}
