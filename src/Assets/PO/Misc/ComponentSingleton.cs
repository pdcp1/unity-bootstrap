using UnityEngine;
using System.Collections;


public class ComponentSingleton<T> : MonoBehaviour where T : Component
{
    private static T instance = null;
	public static T Instance
	{
	    get
	    {
	        InitSingleton();
	        return instance;
	    }
		
		set
		{
			instance = value;	
		}
	}
 
	protected static void InitSingleton()
	{
		if (instance == null)
		{
			instance = (T)FindObjectOfType(typeof(T));
		}
	}
	
    public virtual void Awake ()
    {
		DontDestroyOnLoad(gameObject);
    }
}