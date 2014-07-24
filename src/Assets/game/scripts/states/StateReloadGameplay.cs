using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using po.game;
using InControl;

public class StateReloadGameplay : StateBase 
{

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{

	}

	public override void Enter (StateData data)
	{
		GameManager.Instance.SceneManager.LoadLevel("dummy", null, null, onFadeInComplete);
	}

	public void onFadeInComplete()
	{

		GameManager.Instance.SetState<StateGameplay>();

	}

	public override void Exit ()
	{
		EventDispatcher.Instance.RemoveListener<UIEvent>(onUIEvent);
	}

	void onUIEvent(UIEvent e)
	{
		switch (e.key) 
		{
			case "restart":
				
				GameManager.Instance.SetState<StateGameplay>();
				
				break;
		}
	}
}
