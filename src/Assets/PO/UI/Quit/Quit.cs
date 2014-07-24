using UnityEngine;
using System.Collections;

public class Quit : ComponentSingleton<Quit> 
{

	void Start()
	{
		InitSingleton();
		
		EventDispatcher.Instance.AddListener<UIEvent>(onUIEvent);
		
		Hide();
	}
	
	public void Show()
	{
		gameObject.SetActive(true);
		GameObject.Find("nope").GetComponent<UIButton>().Focus();
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	
	
	void onUIEvent(UIEvent e)
	{
		switch(e.key)
		{
			case "exit-yes":
				 Application.Quit();
			break;
			case "exit-no":
				Hide();
			break;
		}
	}
	
	void OnDestroy()
	{
		EventDispatcher.Instance.RemoveListener<UIEvent>(onUIEvent);
	}
}
