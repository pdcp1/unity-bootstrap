using UnityEngine;
using System.Collections;

public class POtk2dContainter : MonoBehaviour 
{
 	private static POtk2dContainter instance = null;
	public static POtk2dContainter Instance
	{
	    get
	    {
	        if (instance == null)
			{
				instance = (POtk2dContainter)FindObjectOfType(typeof(POtk2dContainter));
				instance.tkCamera = instance.gameObject.GetComponentInChildren<tk2dCamera>();
				DontDestroyOnLoad(instance.gameObject);			
			}   
			
	        return instance;
	    }
		
		set
		{
			instance = value;	
		}
	}
	
	public tk2dCamera tkCamera;
	
	void Awake()
	{
		if(Instance != this)
		{
			Destroy(gameObject);
		}
	}
	
}