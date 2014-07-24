using UnityEngine;
using System;

namespace po.game
{
	public abstract class StateBase:MonoBehaviour
	{
		abstract public void Enter(StateData data);
		abstract public void Exit();
	}
}

