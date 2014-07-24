using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using po.game;

public class StateDebugger : MonoBehaviour {
	
	//List<GameState> states;
	public StateBase[] states;
	public bool debug = false;
	
	// Use this for initialization
	void Start () 
	{
		states = GetComponents<StateBase>();
	}
	
	Vector2 scrollPosition = Vector2.zero;
	
	void OnGUI ()
    {
		var levelButton = new Rect(0, 0, 200, 30);
		var screenPadding = 10;
		
		if(debug)
		{
	  		scrollPosition = GUI.BeginScrollView(new Rect(screenPadding, screenPadding, levelButton.width +  20, Screen.height - screenPadding), scrollPosition, new Rect(0, 0, levelButton.width, states.Length * levelButton.height));
			
				for (int i = 0; i < states.Length; i++) 
				{
					Rect tmpButton = new Rect(0, i * levelButton.height, levelButton.width, levelButton.height );
					
				
					if ( GUI.Button( tmpButton, string.Format("[{0}]", states[i]) ) )      // LOOK HERE
					{					
					//	GameManager.Instance.SetState( states[i].GetType() );
					}	
					
				}	
			
	        GUI.EndScrollView();
		}
    }
	
}
