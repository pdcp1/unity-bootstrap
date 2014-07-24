using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PhoneManager : MonoBehaviour
{
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

#if (UNITY_WP8 || UNITY_ANDROID || UNITY_EDITOR)
		
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			EventDispatcher.Instance.Raise<UIEvent>(new UIEvent{type = "phone", key = "back"});				
		}
			
#endif
	}
}
