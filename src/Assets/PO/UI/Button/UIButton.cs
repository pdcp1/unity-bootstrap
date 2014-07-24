using UnityEngine;
using System.Collections;

public class UIButton : MonoBehaviour
{
	public tk2dUIItem uiItem;

	public string eventKey;
	public UIButton focusDown;
	public UIButton focusLeft;
	public UIButton focusUp;
	public UIButton focusRight;
	
	public tk2dSlicedSprite slicedSprite;

	static private UIButton focused;


	void OnEnable()
	{
		uiItem = GetComponent<tk2dUIItem>();

		if(uiItem == null)
		{
			uiItem = GetComponentInChildren<tk2dUIItem>();
		}

		slicedSprite = uiItem.GetComponent<tk2dSlicedSprite>();

		uiItem.OnDown += onDown;
		uiItem.OnUp += onUp; 
	}
	
	void OnDisable()
	{
		uiItem.OnDown -= onDown;
		uiItem.OnUp -= onUp;

		if(GameManager.Instance != null && GameManager.Instance.Joystick != null)
		{
			GameManager.Instance.Joystick.OnButtonDown -= OnButtonDown;
			GameManager.Instance.Joystick.OnButtonUp -= OnButtonUp;
		}
	}
	
	protected virtual void onDown()
	{		
		Down();
	}
	
	protected virtual void onUp()
	{		
		Up();
	}
	
	public virtual void Down()
	{
		transform.scaleTo(0.06f, Vector3.one * 0.9f);

		//GameManager.Instance.AudioManager.PlayFX(GameManager.FX_BUTTON_CLICK);
	}
	
	public virtual void Up()
	{
		transform.scaleTo(0.06f, Vector3.one);
		EventDispatcher.Instance.Raise<UIEvent>(new UIEvent{type = "down", control = this, key = eventKey ?? null });				
	}


	private void OnButtonDown(JoystickButtons button)
	{
		UIButton next = null;


		switch (button) 
		{
		case JoystickButtons.DL:

			next = focusLeft;
			break;

		case JoystickButtons.DR:

			next = focusRight;
			break;

		case JoystickButtons.DU:

			next = focusUp;
			break;

		case JoystickButtons.DD:

			next = focusDown;
			break;


		case JoystickButtons.B1:

			Down();
			break;
		}

		
		if(next != null)
		{
			next.Focus();
		}
	}

	private void OnButtonUp(JoystickButtons button)
	{
		switch (button) 
		{
		case JoystickButtons.B1:
			
			Up();
			break;
		}
	}
	
	public virtual void Focus()
	{
#if OUYA
		if(focused!= null)
		{
			focused.UnFocus();
		}
		
		focused = this; 
		slicedSprite.SetSprite( slicedSprite.CurrentSprite.name + "-focus" );

		GameManager.Instance.Joystick.OnButtonDown += OnButtonDown;
		GameManager.Instance.Joystick.OnButtonUp += OnButtonUp;
#endif
	}
	
	public virtual void UnFocus()
	{
#if OUYA
		focused = null; 
		slicedSprite.SetSprite( slicedSprite.CurrentSprite.name.Replace("-focus", "") );

		GameManager.Instance.Joystick.OnButtonDown -= OnButtonDown;
		GameManager.Instance.Joystick.OnButtonUp -= OnButtonUp;
#endif
	}
}
