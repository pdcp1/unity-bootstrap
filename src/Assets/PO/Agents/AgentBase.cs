using UnityEngine;
using System.Collections;

public abstract class AgentBase : MonoBehaviour 
{	
	// state management
	AgentState currentState;
	
	public void SetState<S>(StateData data = null) where S : AgentState
	{
		if(currentState!= null)
		{
			currentState.enabled = false;
			currentState.Exit();						
		}	
		
		currentState = gameObject.GetComponent<S>();	
		
		currentState.enabled = true;
		currentState.Enter(data);			
	}
}
