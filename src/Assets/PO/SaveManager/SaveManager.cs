using System;
using UnityEngine;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class SaveManager: MonoBehaviour
{
	Dictionary<Type, List<PropertyInfo>> properties =  new Dictionary<Type, List<PropertyInfo>>();
	
	List<PropertyInfo> getProperties(Type type)
	{
		if( !properties.ContainsKey(type))
		{
			var allProps = type.GetProperties(BindingFlags.Public | BindingFlags.Instance);
			List<PropertyInfo> saveProps =  new List<PropertyInfo>();
			
			for (int i = 0; i < allProps.Length; i++) 
			{
				var prop = allProps[i];
				
				if( !(prop.Name == "Key" || prop.Name == "Loaded")) //|| prop.Name == "Id") )
				{
					saveProps.Add(prop);
				}
			}
			
			properties[type] = saveProps;
		}
		
		return properties[type];
	}
		
	string getGlobalKey(GameSaveBase item, string propertyName)
	{
		return getGlobalKey(item.GetType(), item.Id, propertyName);
	}
	
	string getGlobalKey(Type type, int id, string propertyName)
	{
		return string.Format("{0}-{1}-{2}", type.ToString(), id, propertyName);
	}
	
	public int LastId<T>() where T : GameSaveBase
	{
		return LastId(typeof(T));
	}
	
	public int LastId(Type type)
	{
		var last = 0;
		string key = type.ToString();
		
		if(PlayerPrefs.HasKey(key))
		{
			last = PlayerPrefs.GetInt(key);
		}
		else
		{
			PlayerPrefs.SetInt(key, 0);	
		}
		
		return last;
	}
	
	
	public bool Exists(Type type, int Id)
	{
		return PlayerPrefs.HasKey(getGlobalKey(type, Id, "Id"));
	}
	
	public void Save(GameSaveBase item)
	{		
		var type = item.GetType();
		
		if(item.Id == -1)
		{
			item.Id = LastId(type) + 1;			
			PlayerPrefs.SetInt(type.ToString(), item.Id);
		}
		
		var properties  = getProperties(type);
		
		foreach (var property in properties) 
		{
			var val = property.GetValue(item, null);
			var key = getGlobalKey(item, property.Name);
			var code = Type.GetTypeCode(property.PropertyType);
			
			switch (code) 
			{
				case TypeCode.Int32:
					PlayerPrefs.SetInt( key, (int) val);					
				break;
				case TypeCode.Boolean:
					PlayerPrefs.SetInt( key, ((bool)val)? 1 : 0);					
				break;
				case TypeCode.String:
					PlayerPrefs.SetString( key, (string) val);					
				break;
				case TypeCode.Single:
				case TypeCode.Double:
					PlayerPrefs.SetFloat( key, (float) val);					
				break;					
				default:
					Debug.Log("uplalala" + property.Name);
				break;
			}
		}
		
		PlayerPrefs.Save();
	}
	
	public T Get<T>(int id) where T : GameSaveBase, new ()
	{		
		var item = new T();
		var type = typeof(T);
		
		if(Exists(type, id))
		{
			item.Id = id;
			var properties  = getProperties(type);
		
			foreach (var property in properties) 
			{
				var key = getGlobalKey(item, property.Name);
				var code = Type.GetTypeCode(property.PropertyType);
				
				switch (code) 
				{
					case TypeCode.Int32:
						property.SetValue(item, PlayerPrefs.GetInt(key), null);
					break;
					case TypeCode.Boolean:
						property.SetValue(item, PlayerPrefs.GetInt(key) == 1, null);				
					break;
					case TypeCode.String:
						property.SetValue(item, PlayerPrefs.GetString(key), null);				
					break;
					case TypeCode.Single:
					case TypeCode.Double:
						property.SetValue(item, PlayerPrefs.GetFloat(key), null);				
					break;				
					default:
						Debug.Log("uplalala" + property.Name);
					break;
				}
			}
			
			item.Loaded = true;
		}
		
		return item;
	}
	
	public List<T> Get<T>() where T : GameSaveBase, new()
	{
		List<T> list = new List<T>();		
		int count = LastId(typeof(T));

		for (int i = 1; i <= count; i++) 
		{
			var item  = Get<T>(i);

			if(item.Loaded)
			{
				list.Add(item);
			}
		}
		
		return list;
	}
	
	
	public void Delete(GameSaveBase item)
	{		
		var type = item.GetType();
		
		if(item.Id == -1)
		{
		//	item.Id = LastId(type) + 1;			
		//	PlayerPrefs.SetInt(type.ToString(), item.Id);
		}
		
		var properties  = getProperties(type);
		
		foreach (var property in properties) 
		{
			var key = getGlobalKey(item, property.Name);			
			PlayerPrefs.DeleteKey(key);
		}
	}
	
	public void Delete<T>() where T : GameSaveBase, new()
	{
		Type type = typeof(T);
		int count = LastId(type);
	
		for (int i = 1; i <= count; i++) 
		{
			var item  = Get<T>(i);
			if(item.Loaded)
			{
				Delete(item);
			}
		}
		
		PlayerPrefs.DeleteKey(type.ToString());
	}
		
	
	public void ClearData()
	{
		PlayerPrefs.DeleteAll();
		PlayerPrefs.Save();
	}
}
