using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ToggleButton : UIButton 
{
	public GameObject On;
	public GameObject Off;
	
	public bool IsOn = false;
		
	public void Commit()
	{
		if(IsOn)
		{
			SetOn();
		}
		else
		{
			SetOff();
		}
		
	}
	
#if UNITY_EDITOR
	void Update()
	{
		Commit();
	}
#endif

	override public void Down()
	{
		Toggle();
		base.Down();
	}
	
	
	public void Toggle()
	{		
		IsOn = !IsOn;
		Commit();
	}
	
	public void SetOn()
	{
		IsOn = true;
		On.SetActive(true);
		Off.SetActive(false);
	}
	
	public void SetOff()
	{
		IsOn = false;
		On.SetActive(false);
		Off.SetActive(true);
	}
}
