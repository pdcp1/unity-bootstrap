using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class SceneManager : MonoBehaviour
{	

	tk2dSprite fadeSprite;
	tk2dSprite loadingSprite;

	GoTweenFlow fadeIn;
	GoTweenFlow fadeOut;
	GoTweenConfig fadeOutCfg;
	GoTweenConfig fadeInCfg;

	bool loading = false;
	List<string> levelsToLoad;
	string lastLevelToLoad;

	Action onLoadComplete;

	public void Awake ()
	{
		fadeOutCfg = new GoTweenConfig()
								.colorProp("color", new Color(0f, 0f, 0f, 0f));		
		fadeInCfg = new GoTweenConfig()
								.colorProp("color", new Color(1f, 1f, 1f, 1f));

		fadeSprite = transform.FindChild("fade").GetComponent<tk2dSprite>();
		loadingSprite = transform.FindChild("loading").GetComponent<tk2dSprite>();

		fadeSprite.color = Color.clear;
		loadingSprite.color = Color.clear;

		fadeSprite.gameObject.SetActive(false);
		loadingSprite.gameObject.SetActive(false);
	}

	public void LoadLevel(string level, Action onLoadComplete = null, Action onFadeOutComplete = null, Action onFadeInComplete = null)
	{
		LoadLevel(new List<string>(){ level }, onLoadComplete, onFadeOutComplete, onFadeInComplete);
	}
	
	public void LoadLevel(List<string> levels, Action onLoadComplete = null, Action onFadeOutComplete = null, Action onFadeInComplete = null)
	{
		if(loading)
		{
			Debug.LogError("Scene manager already loading");
//			return;
		}

		loading = true;
		levelsToLoad = levels;
		lastLevelToLoad = levelsToLoad[levelsToLoad.Count-1];

		if(fadeIn != null)
			fadeIn.destroy();
		if(fadeOut != null)
			fadeOut.destroy();
		
		fadeIn = new GoTweenFlow();
		fadeIn.insert(0f, new GoTween(fadeSprite, 0.4f, fadeInCfg ));
		fadeIn.insert(0f, new GoTween(loadingSprite, 0.2f, fadeInCfg ));
		
		fadeOut	= new GoTweenFlow();
		fadeOut.insert(0f, new GoTween(fadeSprite, 0.4f, fadeOutCfg ));
		fadeOut.insert(0f, new GoTween(loadingSprite, 0.2f, fadeOutCfg ));
	
		
		this.onLoadComplete = onLoadComplete;
		
		fadeIn.setOnStartHandler( t => 
		{
			fadeSprite.gameObject.SetActive(true);
			loadingSprite.gameObject.SetActive(true);	
		});
		
		fadeIn.setOnCompleteHandler( (tween) => 
		{
			if(onFadeInComplete != null)
				onFadeInComplete();

			//this is synchronous
			foreach (var item in levels)
			{
				Application.LoadLevel(item);			
			}

			loading = false;
		});
		
		fadeOut.setOnCompleteHandler( (tween) => 
		{
			if(onFadeOutComplete != null)
				onFadeOutComplete();
			
			fadeSprite.gameObject.SetActive(false);
			loadingSprite.gameObject.SetActive(false);
		});
		
		fadeIn.play();
	}
	
	void OnLevelWasLoaded(int level) 
	{
		Debug.Log("Level was loaded:" + Application.loadedLevelName);

		if(Application.loadedLevelName.Equals(lastLevelToLoad))
		{
			Debug.Log("Complete: " + Application.loadedLevelName);

			if(onLoadComplete != null)
				onLoadComplete();
			
			onLoadComplete = null;			
			fadeOut.play();

		}
	}
}
