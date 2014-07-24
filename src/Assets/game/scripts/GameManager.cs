using UnityEngine;
using System.Collections;
using po.game;

public class GameManager : GameManagerBase<GameManager>
{
	static public readonly int MUSIC_MENU = 0;
	
	static public readonly int FX_BUTTON_CLICK = 0;
	static public readonly int FX_BUTTON_HIGH_CLICK = 1;
	static public readonly int FX_BUTTON_LOCKED = 2;

	void Awake()
	{
		base.OnAwake();				
	}
	
	void Start ()   
	{
		base.OnAwake();
		SetState<StateMaster>();
	}
}


