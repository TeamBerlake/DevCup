using UnityEngine;
using System.Collections;

public class Shape : MonoBehaviour {

	#region Enums

	protected enum MoveState
	{
		INACTIVE,
		ACTIVE
	}

	protected enum MoveDirection
	{
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	public enum ShapeColor
	{
		RED,
		YELLOW,
		BLUE,
		GREEN,
		ORANGE,
		PURPLE,
		GREY
	}

	public static Color ToColor (ShapeColor sc)
	{
		switch (sc)
		{
		case ShapeColor.BLUE:
			return Color.blue;
		case ShapeColor.GREEN:
			return Color.green;
		case ShapeColor.GREY:
			return Color.gray;
		case ShapeColor.PURPLE:
			return new Color(1.0f,0.0f,1.0f,1.0f);
		case ShapeColor.RED:
			return Color.red;
		case ShapeColor.ORANGE:
			return new Color(1.0f,0.5f,0.0f,1.0f);
		default:
			return Color.white;
		}
	}

	#endregion // Enums

	#region Universal Variables

	[SerializeField]
	protected	ShapeColor	m_color			= ShapeColor.GREY;
	protected	bool		m_canBeMoved	= true;

	#endregion // Universal Variables

	#region Monobehavior Functions

	// Use this for initialization
	void Start () 
	{
		if (m_color == ShapeColor.GREY)
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
		// Default behavior
		Shape shape = col.transform.parent.GetComponent<Shape>();
		if (shape == null)
			return;

		if(!shape.CanBeMoved)
		{
			shape.CurrentColor = m_color;
			shape.CanBeMoved = true;
		}

		ShapeColor oldColor = m_color;
		MixColor(shape.CurrentColor);
		shape.MixColor(oldColor);

		_OnTriggerEnter(col);
	}

	void OnTriggerExit ()
	{
		_OnTriggerExit();
	}

	protected virtual void _Update ()
	{

	}

	protected virtual void _OnTriggerEnter (Collider col)
	{

	}

	protected virtual void _OnTriggerExit ()
	{

	}

	#endregion // Monobehavior Functions

	#region Private Functions

	private ShapeColor _CombineColors (ShapeColor color1, ShapeColor color2)
	{
		ShapeColor product = color1;
		if (color1 == ShapeColor.RED)
		{
			switch (color2)
			{
			case ShapeColor.BLUE:
				product = ShapeColor.PURPLE;
				break;
			case ShapeColor.YELLOW:
				product = ShapeColor.ORANGE;
				break;
			default:
				break;
			}
		}
		else if (color1 == ShapeColor.YELLOW)
		{
			switch (color2)
			{
			case ShapeColor.BLUE:
				product = ShapeColor.GREEN;
				break;
			case ShapeColor.RED:
				product = ShapeColor.ORANGE;
				break;
			default:
				break;
			}
		}
		else if (color1 == ShapeColor.BLUE)
		{
			switch (color2)
			{
			case ShapeColor.YELLOW:
				product = ShapeColor.GREEN;
				break;
			case ShapeColor.RED:
				product = ShapeColor.PURPLE;
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
			if (value == ShapeColor.GREY)
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
			m_color = ShapeColor.GREY;
		}

		if (this.gameObject != null)
		{
			gameObject.transform.position = initialPosition;
		}
	}

	#endregion // Public Functions
}
