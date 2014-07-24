using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneDebugger : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public bool debug = false;	
	public List<string> Levels = new List<string>();
	Vector2 scrollPosition = Vector2.zero;
	
	void OnGUI ()
    {
		var levelButton = new Rect(0, 0, 100, 30);
		var screenPadding = 10;
		
		if(debug)
		{
	  		scrollPosition = GUI.BeginScrollView(new Rect(screenPadding, screenPadding, levelButton.width +  20, Screen.height - screenPadding), scrollPosition, new Rect(0, 0, levelButton.width, Levels.Count * levelButton.height));
			
				for (int i = 0; i < Levels.Count; i++) 
				{
					Rect tmpButton = new Rect(0, i * levelButton.height, levelButton.width, levelButton.height );
					
					if ( GUI.Button( tmpButton, string.Format("[{0}]", Levels[i]) ) )      // LOOK HERE
					{
						GameManager.Instance.SceneManager.LoadLevel(Levels[i], () => {});
					}	
				}	
			
	        GUI.EndScrollView();
		}
    }
	
}
