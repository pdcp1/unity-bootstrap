using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using InControl;

public class PoInControlJoystick : JoystickBase 
{
	public override event System.Action<JoystickButtons> OnButtonDown;
	public override event System.Action<JoystickButtons> OnButtonUp;

	override protected void Awake()
	{
		base.Awake();
#if !OUYA
		Destroy(gameObject);
#endif
	}
	void Start () 
	{
		InputManager.Setup();
	}

	

	public override void Enable()
	{
		// events

	}
	
	public override void Disable()
	{

	}

	public override void Show(float duration)
	{

	}
	
	public override void Hide(float duration)
	{

	}

	/*
	public override float GetAxis (string name)
	{
		float ret = 0f;
		
		switch(name)
		{
		case "Horizontal":	


			ret = ( Mathf.Abs(OuyaInput.GetAxis(OuyaAxis.RX, (OuyaPlayer) currentPlayer)) > 0.1f ) 
					? OuyaInput.GetAxis(OuyaAxis.RX, (OuyaPlayer) currentPlayer)
					: OuyaInput.GetAxis(OuyaAxis.DX, (OuyaPlayer) currentPlayer);

			break;
		}
		
		return ret;
	}
*/
	public override bool IsActive()
	{	
		return false;
	}

	public override bool GetButtonDown(int button)
	{

		switch((JoystickButtons)button)
		{
			case JoystickButtons.B1: return InputManager.ActiveDevice.Action1.WasPressed;
			case JoystickButtons.B2: return InputManager.ActiveDevice.Action3.WasPressed;
			case JoystickButtons.B3: return InputManager.ActiveDevice.Action4.WasPressed;
			case JoystickButtons.B4: return InputManager.ActiveDevice.Action2.WasPressed;

			case JoystickButtons.DL: return InputManager.ActiveDevice.DPadLeft.WasPressed;
			case JoystickButtons.DR: return InputManager.ActiveDevice.DPadRight.WasPressed;
			case JoystickButtons.DU: return InputManager.ActiveDevice.DPadUp.WasPressed;
			case JoystickButtons.DD: return InputManager.ActiveDevice.DPadDown.WasPressed;
		}

		return false;
	}

	public override bool GetButtonUp(int button)
	{
		switch((JoystickButtons)button)
		{
		case JoystickButtons.B1: return InputManager.ActiveDevice.Action1.WasReleased;
		case JoystickButtons.B2: return InputManager.ActiveDevice.Action3.WasReleased;
		case JoystickButtons.B3: return InputManager.ActiveDevice.Action4.WasReleased;
		case JoystickButtons.B4: return InputManager.ActiveDevice.Action2.WasReleased;
		
		case JoystickButtons.DL: return InputManager.ActiveDevice.DPadLeft.WasReleased;
		case JoystickButtons.DR: return InputManager.ActiveDevice.DPadRight.WasReleased;
		case JoystickButtons.DU: return InputManager.ActiveDevice.DPadUp.WasReleased;
		case JoystickButtons.DD: return InputManager.ActiveDevice.DPadDown.WasReleased;
		}	

		return false;
	}
	
	public override bool GetButton(int button)
	{
		switch((JoystickButtons)button)
		{
		case JoystickButtons.B1: return InputManager.ActiveDevice.Action1.State;
		case JoystickButtons.B2: return InputManager.ActiveDevice.Action3.State;
		case JoystickButtons.B3: return InputManager.ActiveDevice.Action4.State;
		case JoystickButtons.B4: return InputManager.ActiveDevice.Action2.State;

		case JoystickButtons.DL: return InputManager.ActiveDevice.DPadLeft.State;
		case JoystickButtons.DR: return InputManager.ActiveDevice.DPadRight.State;
		case JoystickButtons.DU: return InputManager.ActiveDevice.DPadUp.State;
		case JoystickButtons.DD: return InputManager.ActiveDevice.DPadDown.State;
		}		

		return false;
	}

	void Update() 
	{
		InputManager.Update();

		bool dpadLeft;
		bool dpadRight;
		bool dpadUp;
		bool dpadDown;
		
		bool O;
		bool U;
		bool Y;
		bool A;
		
		if(OnButtonDown != null)
		{
			
			//down
			dpadLeft = InputManager.ActiveDevice.DPadLeft.WasPressed;
			dpadRight = InputManager.ActiveDevice.DPadRight.WasPressed;
			dpadUp = InputManager.ActiveDevice.DPadUp.WasPressed;
			dpadDown = InputManager.ActiveDevice.DPadDown.WasPressed;
			
			O = InputManager.ActiveDevice.Action1.WasPressed;
			U = InputManager.ActiveDevice.Action3.WasPressed;
			Y = InputManager.ActiveDevice.Action4.WasPressed;
			A = InputManager.ActiveDevice.Action2.WasPressed;
			
			if(dpadLeft)
			{
				OnButtonDown(JoystickButtons.DL);
			}
			
			if(dpadRight)
			{
				OnButtonDown(JoystickButtons.DR);
			}
			
			if(dpadUp)
			{
				OnButtonDown(JoystickButtons.DU);
			}
			
			if(dpadDown)
			{
				OnButtonDown(JoystickButtons.DD);
			}
			
			
			
			if(O)
			{
				OnButtonDown(JoystickButtons.B1);
			}
			
			if(U)
			{
				OnButtonDown(JoystickButtons.B2);
			}
			
			if(Y)
			{
				OnButtonDown(JoystickButtons.B3);
			}
			
			if(A)
			{
				OnButtonDown(JoystickButtons.B4);
			}
		}
		
		if(OnButtonUp != null)
		{
			//up
			dpadLeft = InputManager.ActiveDevice.DPadLeft.WasReleased;
			dpadRight = InputManager.ActiveDevice.DPadRight.WasReleased;
			dpadUp = InputManager.ActiveDevice.DPadUp.WasReleased;
			dpadDown = InputManager.ActiveDevice.DPadDown.WasReleased;
			
			O = InputManager.ActiveDevice.Action1.WasReleased;
			U = InputManager.ActiveDevice.Action3.WasReleased;
			Y = InputManager.ActiveDevice.Action4.WasReleased;
			A = InputManager.ActiveDevice.Action2.WasReleased;
			
			
			if(dpadLeft)
			{
				OnButtonUp(JoystickButtons.DL);
			}
			
			if(dpadRight)
			{
				OnButtonUp(JoystickButtons.DR);
			}
			
			if(dpadUp)
			{
				OnButtonUp(JoystickButtons.DU);
			}
			
			if(dpadDown)
			{
				OnButtonUp(JoystickButtons.DD);
			}
			
			if(O)
			{
				OnButtonUp(JoystickButtons.B1);
			}
			
			if(U)
			{
				OnButtonUp(JoystickButtons.B2);
			}
			
			if(Y)
			{
				OnButtonUp(JoystickButtons.B3);
			}
			
			if(A)
			{
				OnButtonUp(JoystickButtons.B4);
			}
		}
	}
}
