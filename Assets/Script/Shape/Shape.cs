using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	#region Enums

	protected enum MoveState
	{
		INACTIVE,
		ACTIVE
	}

	public enum ShapeColor
	{
		Red,
		Yellow,
		Blue,
		Green,
		Orange,
		Purple,
		Grey
	}

	public static Color ToColor (ShapeColor sc)
	{
		switch (sc)
		{
		case ShapeColor.Blue:
			return Color.blue;
		case ShapeColor.Green:
			return Color.green;
		case ShapeColor.Grey:
			return Color.gray;
		case ShapeColor.Purple:
			return new Color(1.0f,0.0f,1.0f,1.0f);
		case ShapeColor.Red:
			return Color.red;
		case ShapeColor.Orange:
			return new Color(1.0f,0.5f,0.0f,1.0f);
		default:
			return Color.white;
		}
	}

	#endregion // Enums

	#region Universal Variables

	[SerializeField]
	protected	ShapeColor	m_color			= ShapeColor.Grey;
	protected	bool		m_canBeMoved	= true;

	#endregion // Universal Variables

	#region Monobehavior Functions

	// Use this for initialization
	void Start () 
	{
		if (m_color == ShapeColor.Grey)
			m_canBeMoved = false;
		else
			m_canBeMoved = true;
	}
	
	// Update is called once per frame
	void Update ()
	{
		foreach (Transform t in transform.GetComponentsInChildren<Transform>())
		{
			if (t.renderer != null)
				t.renderer.material.color = ToColor(m_color);
		}
		
		if (!Main.Instance.IsPlaying)
			return;
		_Update();
	}

	void OnTriggerEnter (Collider col)
	{
		Shape shape = col.transform.parent.GetComponent<Shape>();
		if (shape == null)
			return;

		if(!shape.CanBeMoved)
		{
			shape.CurrentColor = m_color;
			shape.CanBeMoved = true;
		}

		ShapeColor woot = m_color;
		MixColor(shape.CurrentColor);
		shape.MixColor(woot);
	}

	protected virtual void _Update ()
	{

	}

	#endregion // Monobehavior Functions

	#region Private Functions

	private ShapeColor _CombineColors (ShapeColor color1, ShapeColor color2)
	{
		ShapeColor product = color1;
		if (color1 == ShapeColor.Red)
		{
			switch (color2)
			{
			case ShapeColor.Blue:
				product = ShapeColor.Purple;
				break;
			case ShapeColor.Yellow:
				product = ShapeColor.Orange;
				break;
			default:
				break;
			}
		}
		else if (color1 == ShapeColor.Yellow)
		{
			switch (color2)
			{
			case ShapeColor.Blue:
				product = ShapeColor.Green;
				break;
			case ShapeColor.Red:
				product = ShapeColor.Orange;
				break;
			default:
				break;
			}
		}
		else if (color1 == ShapeColor.Blue)
		{
			switch (color2)
			{
			case ShapeColor.Yellow:
				product = ShapeColor.Green;
				break;
			case ShapeColor.Red:
				product = ShapeColor.Purple;
				break;
			default:
				break;
			}
		}

		return product;
	}

	#endregion // Private Functions

	#region Public Functions

	public bool CanBeMoved
	{
		get {return m_canBeMoved;}
		set {m_canBeMoved = value;}
	}

	public ShapeColor CurrentColor
	{
		get {return m_color;}
		set 
		{
			m_color = value;
			if (value == ShapeColor.Grey)
				m_canBeMoved = false;
		}
	}

	public void MixColor (ShapeColor s)
	{
		m_color = _CombineColors(m_color, s);
	}

	public void Initialize (Vector3 initialPosition, ShapeColor color, bool canBeMoved)
	{
		m_canBeMoved = canBeMoved;

		if (m_canBeMoved)
		{
			m_color = color;
		}
		else
		{
			m_color = ShapeColor.Grey;
		}

		if (this.gameObject != null)
		{
			gameObject.transform.position = initialPosition;
		}
	}

	#endregion // Public Functions
}
