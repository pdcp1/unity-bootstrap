using UnityEngine;
using System.Collections;

static public class Vector3Extensions
{
	public static Vector3 Clone(this Vector3 v)
	{
	    return new Vector3(v.x, v.y, v.z);
	}
}