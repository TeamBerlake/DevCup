using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public delegate void DInput ();
public class Main : MonoBehaviour
{
	private static int s_currState = 0;
	private static string s_status = "PLAY";
	private static bool s_nextLevelUnlocked = false;
	private static Main	s_main = null;
	private static int	s_currentLevel = 0;
	private static Dictionary<KeyCode,DInput> s_inputs	= null;

	public bool IsPlaying
	{
		get {return s_currState == 1;}
	}
	
	public void OpenNextLevel ()
	{
		s_nextLevelUnlocked = true;
	}

	public static Main Instance
	{
		get {return s_main;}
	}

	public void BindInput (KeyCode code, DInput dG)
	{
		if (s_inputs.ContainsKey(code))
			s_inputs[code] += dG;
		else
			s_inputs.Add(code, dG);
	}
	
	public void RemoveInput (KeyCode code, DInput dG)
	{
		if (s_inputs.ContainsKey(code))
			s_inputs[code] -= dG;
	}

	void Awake ()
	{
		s_main = this;
		s_currentLevel = Application.loadedLevel;
		s_inputs = new Dictionary<KeyCode, DInput>();
		DontDestroyOnLoad(this);
		_Reset();
	}

	void Update ()
	{
		if (s_currState == 0)
			return;

		foreach (KeyValuePair<KeyCode, DInput> code in s_inputs)
		{
			if (Input.GetKeyDown(code.Key))
				code.Value();
		}
	}

	#region GUI CRAP

	void OnGUI ()
	{
		if (GUI.Button(new Rect(Screen.width - 80, 0, 80, 80), s_status))
		{
			if (IsPlaying)
				_Stop ();
			else
				_Play ();
		}
		
		if (s_nextLevelUnlocked)
		{
			if (GUI.Button(new Rect(Screen.width - 240, 0, 160, 80), "NEXT LEVEL"))
				_NextLevel();
		}
	}

	private void _Play ()
	{
		s_currState = 1;
		s_status = "STOP";
	}
	
	private void _Stop ()
	{
		s_currState = 0;
		s_status = "PLAY";
		_Reset();
	}
	
	private void _NextLevel ()
	{
		if (s_currentLevel < Application.levelCount-1)
		{
			s_inputs = new Dictionary<KeyCode,DInput>();
			Application.LoadLevel(++s_currentLevel);
			s_nextLevelUnlocked = false;
		}
	}

	private void _Reset ()
	{
		GameObject obj = GameObject.Find("Level"+s_currentLevel+"(Clone)");
		if (obj != null)
			GameObject.Destroy(obj);

		GameObject obj2 = GameObject.Find("Level"+s_currentLevel);
		if (obj2 != null)
			GameObject.Destroy(obj2);

		GameObject newObj = Resources.Load("Prefabs/Level"+s_currentLevel) as GameObject;
		GameObject.Instantiate(newObj);
	}

	#endregion // GUI CRAP


}
