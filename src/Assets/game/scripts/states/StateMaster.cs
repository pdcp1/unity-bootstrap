using UnityEngine;
using System.Collections;
using po.game;

public class StateMaster : StateBase 
{
	// Use this for initialization
	void Start () {

		Debug.Log("State master loaded!");
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Enter (StateData data)
	{
		//throw new System.NotImplementedException();

		GameManager.Instance.SetState<StateGameplay>();
	}

	public override void Exit ()
	{
		//throw new System.NotImplementedException();
	}
}
