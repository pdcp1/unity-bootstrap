using UnityEngine;
using System.Collections;

public class Singleton<T> where T : class, new()
{
	public static readonly T Instance = new T ();
}