using UnityEngine;
using System.Collections;
using InControl;

public class AccelerometerSource : InputControlSource 
{
	string accelerometerAxis;
	
	public AccelerometerSource( string axis )
	{
		this.accelerometerAxis = axis;
	}
	
	public override float GetValue( InputDevice inputDevice )
	{
		return (accelerometerAxis.Equals("Horizontal")) ? Input.acceleration.x : Input.acceleration.y;
	}
	
	public override bool GetState( InputDevice inputDevice )
	{
		return !Mathf.Approximately( GetValue( inputDevice ), 0.0f );
	}
}
