using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using po.game;
using InControl;

public class StateGameplay : StateBase 
{
	public override void Enter (StateData data)
	{
		GameManager.Instance.SceneManager.LoadLevel("gameplay", onLoadComplete);
	}

	public void onLoadComplete()
	{
		EventDispatcher.Instance.AddListener<UIEvent>(onUIEvent);
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
				
				GameManager.Instance.SetState<StateReloadGameplay>();
				
				break;
		}
	}
}
