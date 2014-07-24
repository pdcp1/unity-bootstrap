using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace po.utils
{

	public static class Utils
	{
		static public List<Type> GetEnumerableOfType<T>()
	    {
	        List<Type> objects = new List<Type>();
			
	        foreach (Type type in Assembly.GetAssembly(typeof(T)).GetTypes())
	        {
				if(type.IsAssignableFrom(typeof(T)))
				{
					objects.Add(type);	
				}
	        }

	        return objects;
	    }		
	}
}