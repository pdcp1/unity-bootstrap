using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class tk2dJoystick : JoystickBase
{
	public tk2dUIItem touchpadLeft;
	public tk2dUIItem touchpadRight;
	public tk2dUIItem button;  
	
	public bool EnableKeyboard = true;
	
	public override event System.Action<JoystickButtons> OnButtonDown;
	public override event System.Action<JoystickButtons> OnButtonUp;

	Dictionary<string, bool> buttons = new Dictionary<string, bool>();	
	Dictionary<string, bool> lastButtons = new Dictionary<string, bool>();

	GoTweenConfig fadeIn;
	GoTweenConfig fadeOut;


	override protected void Awake()
	{
#if OUYA
		Destroy(gameObject);
#endif
	}
	
	// Use this for initialization
	void Start () 
	{	

		//tweens		
		fadeIn = new GoTweenConfig()
						.colorProp("color", colorReleased);

		fadeOut =  new GoTweenConfig()
								.colorProp("color", new Color(1f, 1f, 1f, 0f));

		//dictionary setup
		buttons.Add("left", false);
		buttons.Add("jump", false);
		buttons.Add("right", false);
		
		lastButtons.Add("left", false);
		lastButtons.Add("jump", false);
		lastButtons.Add("right", false);
	}
	
	
	GoTweenFlow showFlow;
	GoTweenFlow hideFlow;
	
	public override void Show (float duration)
	{
		ClearFlows();

		showFlow = new GoTweenFlow();
		
		showFlow.insert(0, new GoTween(touchpadLeft.GetComponent<tk2dSprite>(), duration, fadeIn));
		showFlow.insert(0, new GoTween(touchpadRight.GetComponent<tk2dSprite>(), duration, fadeIn));
		showFlow.insert(0, new GoTween(button.GetComponent<tk2dSprite>(), duration, fadeIn));
		
		showFlow.setOnCompleteHandler(t => 
		{
			gameObject.SetActive(true);
		});
		
		showFlow.play();
	}
	
	public override void Hide (float duration)
	{
		ClearFlows();

		hideFlow = new GoTweenFlow();
		
		hideFlow.insert(0, new GoTween(touchpadLeft.GetComponent<tk2dSprite>(), duration, fadeOut));
		hideFlow.insert(0, new GoTween(touchpadRight.GetComponent<tk2dSprite>(), duration, fadeOut));
		hideFlow.insert(0, new GoTween(button.GetComponent<tk2dSprite>(), duration, fadeOut));
		
		hideFlow.setOnCompleteHandler(t => 
		{
			gameObject.SetActive(false);
		});
		
		hideFlow.play();
	}
	
	void ClearFlows()
	{
		if(hideFlow != null)
			hideFlow.destroy();
		
		if(showFlow != null)
			showFlow.destroy();
	}
		
	public override void Enable()
	{
		// events
		button.OnDownUIItem += onButtonDown;
		button.OnUpUIItem += onButtonUp;
		
		touchpadLeft.OnDownUIItem += onButtonDown;
		touchpadLeft.OnUpUIItem += onButtonUp;

		touchpadRight.OnDownUIItem += onButtonDown;
		touchpadRight.OnUpUIItem += onButtonUp;		
	}
	
	public override void Disable()
	{
		var keys = new List<string>(buttons.Keys);
		//disable button status
		foreach(var key in keys)
		{
			buttons[key] = false;
		}
		// events
		button.OnDownUIItem -= onButtonDown;
		button.OnUpUIItem -= onButtonUp;
		
		touchpadLeft.OnDownUIItem -= onButtonDown;
		touchpadLeft.OnUpUIItem -= onButtonUp;

		touchpadRight.OnDownUIItem -= onButtonDown;
		touchpadRight.OnUpUIItem -= onButtonUp;
	}
	
	Color colorReleased = new Color(1f,1f,1f, 0.5f);
	Color colorPresed = new Color(1f,1f,1f, 0.7f);

	void onButtonDown(tk2dUIItem uiItem)
	{
		buttons[uiItem.gameObject.name] = true;
		uiItem.gameObject.GetComponent<tk2dSprite>().color = colorPresed;

		if(OnButtonDown!= null)
			OnButtonDown(getJoystickButton(uiItem.gameObject.name));
		
	}
	
	void onButtonUp(tk2dUIItem uiItem)
	{
		buttons[uiItem.gameObject.name] = false;
		uiItem.gameObject.GetComponent<tk2dSprite>().color = colorReleased;

		if(OnButtonUp!= null)
			OnButtonUp(getJoystickButton(uiItem.gameObject.name));
	}
	
	public override bool IsActive ()
	{
		foreach(var b in buttons)
		{
			if(b.Value)
			{
				return true;
			}
		}
		
		return false;
	}

	
	public override bool GetButton(int button)
	{

		JoystickButtons but = (JoystickButtons)button;
		bool pressed = false;

		switch(but)
		{
		case JoystickButtons.DL:
			pressed = buttons["left"];
			break;
		case JoystickButtons.DR:
			pressed = buttons["right"];
			break;
		case JoystickButtons.B1:
			pressed = buttons["jump"];
			break;
		}

		return pressed;
	}

	private JoystickButtons getJoystickButton(string name)
	{
		switch(name)
		{
		case "left": return JoystickButtons.DL; break;
		case "right": return JoystickButtons.DR; break;
		case "jump": return JoystickButtons.B1; break;
		}

		return JoystickButtons.B1;
	}

	public override bool GetButtonDown(int button)
	{
		return false;
	}
	
	public override bool GetButtonUp(int button)
	{
		return false;
	}

	

#if UNITY_EDITOR || UNITY_WEBPLAYER 
	
	void Updfdsfsdfate()
	{		
		/*
		if(EnableKeyboard)
		{
			buttons["jump"] = (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W));
			buttons["right"] = (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D));
			buttons["left"] = (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A));


			/*
			if(buttons["jump"] != lastButtons["jump"])


				if(buttons["jump"])
				{
				}
				OnChange("jump", buttons["jump"]);
			
			if(buttons["right"] != lastButtons["right"])
				OnChange("right", buttons["right"]);
			
			if(buttons["left"] != lastButtons["left"])
				OnChange("left", buttons["left"]);
			*/

		/*

			lastButtons["jump"] = buttons["jump"];
			lastButtons["right"] = buttons["right"];
			lastButtons["left"] = buttons["left"];
		}
		
		*/
	}
#endif
}
