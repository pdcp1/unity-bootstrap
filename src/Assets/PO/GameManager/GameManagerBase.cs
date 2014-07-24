using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using po.game;
using System;

namespace po.game
{
	public class GameManagerBase<T> : MonoBehaviour where T : Component
	{
	    private static T instance = null;
		public static T Instance
		{
		    get
		    {
		        if (instance == null)
		            instance = (T)FindObjectOfType(typeof(T));
				
		        return instance;
		    }
			
			set
			{
				instance = value;	
			}
		}
		
		//public managers
		 
		public JoystickBase Joystick;
		public SceneManager SceneManager;
		public SaveManager SaveManager;
		public AudioManager AudioManager;
		public ResolutionManager ResolutionManager;
		
		//events
		
    	protected void OnAwake ()
		{
			DontDestroyOnLoad(gameObject);
		}
		
		
		// state management
		StateBase currentState;
			
		public void SetState<S>(StateData data = null) where S : StateBase
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
		
		public void SetState<S, D>(D data ) where S : StateBase where D : StateData
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

		/*
		public void SetState<D>(Type state, D data = null) where D : StateData
		{
			if(currentState!= null)
			{
				currentState.enabled = false;
				currentState.Exit();			
			}	
			
			currentState = (GameState<D>) gameObject.GetComponent(state);	
			
			currentState.enabled = true;
			currentState.Enter(data);
		}
		*/
	}
}