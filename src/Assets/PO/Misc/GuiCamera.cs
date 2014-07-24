using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class GuiCamera : MonoBehaviour 
{	
	
	void Start () 
	{	
		var Items = GetComponentsInChildren<GUITexture>();		
		
		foreach (var item in Items) 
		{
			item.gameObject.layer = gameObject.layer;	
		}
		
		DontDestroyOnLoad(this);
	}
}
