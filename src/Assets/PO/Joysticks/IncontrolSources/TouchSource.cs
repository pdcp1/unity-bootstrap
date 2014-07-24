using UnityEngine;
using System.Collections;
using InControl;

public class TouchSource : InputControlSource
{

	int buttonId;
	static string[,] buttonQueries;
	
	
	public TouchSource()
	{
//		this.buttonId = buttonId;
//		SetupButtonQueries();
	}
	
	
	public override float GetValue( InputDevice inputDevice )
	{
		return GetState( inputDevice ) ? 1.0f : 0.0f;
	}
	
	
	public override bool GetState( InputDevice inputDevice )
	{
		return Input.touches.Length > 0;
	}
	
//	
//	static void SetupButtonQueries()
//	{
//		if (buttonQueries == null)
//		{			
//			buttonQueries = new string[ UnityInputDevice.MaxDevices, UnityInputDevice.MaxButtons ];
//			
//			for (int joystickId = 1; joystickId <= UnityInputDevice.MaxDevices; joystickId++)
//			{
//				for (int buttonId = 0; buttonId < UnityInputDevice.MaxButtons; buttonId++)
//				{
//					buttonQueries[ joystickId - 1, buttonId ] = "joystick " + joystickId + " button " + buttonId;
//				}
//			}
//		}
//	}
//	
//	
//	static string GetButtonKey( int joystickId, int buttonId )
//	{
//		return buttonQueries[ joystickId - 1, buttonId ];
//	}
}
