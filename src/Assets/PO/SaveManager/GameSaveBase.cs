using UnityEngine;
using System;
using System.Reflection;
using System.Collections.Generic;

public abstract class GameSaveBase
{
	static List<PropertyInfo> properties;

	public int Id { get; set;}
	public bool Loaded { get; set;}
	
	public GameSaveBase()
	{
		Loaded = false;
		Id = -1;
	}
	
	public override string ToString ()
	{
		string props = string.Empty;
		
		props += string.Format ("[GameSaveBase: Id={0}, Loaded={1}]", Id, Loaded);
		
		foreach(var p in this.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
		{			
			props += string.Format("{0} = {1} \n", p.Name, p.GetValue(this, null));
		}
		
		return props;
	}
}