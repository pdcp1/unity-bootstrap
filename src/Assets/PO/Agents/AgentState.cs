using UnityEngine;
using System.Collections;

public abstract class AgentState : MonoBehaviour 
{
	abstract public void Enter(StateData data);
	abstract public void Exit();

	AgentBase agent;

	public AgentBase Agent
	{
		get
		{
			if(agent == null)
			{
				agent = GetComponent<AgentBase>();
			}

			return agent;
		}
	}
}
