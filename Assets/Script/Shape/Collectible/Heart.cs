using UnityEngine;
using System.Collections;

public class Heart : MonoBehaviour
{
	private Color ToColor (Shape.ShapeColor sc)
	{
		switch (sc)
		{
		case Shape.ShapeColor.BLUE:
			return Color.blue;
		case Shape.ShapeColor.GREEN:
			return Color.green;
		case Shape.ShapeColor.GREY:
			return Color.gray;
		case Shape.ShapeColor.PURPLE:
			return new Color(1.0f,0.0f,1.0f,1.0f);
		case Shape.ShapeColor.RED:
			return Color.red;
		case Shape.ShapeColor.ORANGE:
			return new Color(1.0f,0.5f,0.0f,1.0f);
		default:
			return Color.white;
		}
	}

	[SerializeField]
	Shape.ShapeColor color = Shape.ShapeColor.GREY;
	public Shape.ShapeColor HeartColor
	{
		get {return color;}
		set {color = value;}
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

	void Update ()
	{
		foreach (Transform t in transform.GetComponentsInChildren<Transform>())
		{
			if (t.renderer != null)
				t.renderer.material.color = ToColor(color);
		}
	}
}
