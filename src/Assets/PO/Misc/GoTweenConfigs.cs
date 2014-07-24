using UnityEngine;
using System.Collections;

static public class GoTweenConfigs
{
	
	static public GoTweenConfig FadeIn
	{
		get
		{
			return new GoTweenConfig()
						.colorProp("color", new Color(1f, 1f, 1f, 1f));// new Color(0.5f, 0.5f, 0.5f, 0.5f));
		}
	}
	
	static public GoTweenConfig FadeOut
	{
		get
		{
			return new GoTweenConfig()
								.colorProp("color", new Color(1f, 1f, 1f, 0f));	
		}
	}
}
