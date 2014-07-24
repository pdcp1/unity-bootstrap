using UnityEngine;

public class ButtonInfo
{
	bool pressed = false;
	int pressedAtFrame = 0;
	
	public delegate void OnChange(bool pressed);
	public event OnChange onChange;
	
	public void press()
	{
		if(pressedAtFrame == 0)
		{
			pressedAtFrame = Time.frameCount;
			pressed = true;
			fireChange();
		}
	}
	
	public bool isPressed
	{
		get { return pressed; }	
	}
	
	public void change(bool pressed)
	{
		if(pressed)
			press();
		else
			release();
	}
	
	public void release()
	{
		if(pressedAtFrame != 0)
		{
			pressed  = false;
			pressedAtFrame = 0;		
			fireChange();
		}
	}
	
	void fireChange()
	{
		if(onChange != null)
		{
			onChange(pressed);
		}
	}
}