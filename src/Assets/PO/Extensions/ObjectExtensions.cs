using UnityEngine;
using System.Collections;

static public class ObjectExtensions
{	
	public static T[] FindObjectsOfType <T> (this Object unityObject) where T : Object 
	{
	    return Object.FindObjectsOfType(typeof(T)) as T[];
	}
	
	public static T FindObjectOfType<T>(this UnityEngine.Object obj) where T : UnityEngine.Object
    {
        return (UnityEngine.Object.FindObjectOfType(typeof(T)) as T);
    }

}
