using UnityEngine;
using System.Collections;

static public class TransformExtensions
{
	public static Transform findChildren(this Transform transform, string name)
	{
		var children = transform.gameObject.GetComponentsInChildren<Transform>();		
		foreach(var child in children)
		{
			if(child.gameObject.name == name)
				return child;
		}
		
		return null;
	}
	
	//global
	public static void SetX(this Transform t, float x)
	{
	  Vector3 p = new Vector3(x, t.position.y, t.position.z); 
	  t.position = p;
	}
	
	public static void SetY(this Transform t, float y)
	{
	  Vector3 p = new Vector3(t.position.x, y, t.position.z); 
	  t.position = p;
	}
	
	public static void SetZ(this Transform t, float z)
	{
	  Vector3 p = new Vector3(t.position.x, t.position.y, z); 
	  t.position = p;
	}
	
	//local 
	public static void SetLocalX(this Transform t, float x)
	{
	  Vector3 p = new Vector3(x, t.localPosition.y, t.localPosition.z); 
	  t.localPosition = p;
	}
	
	public static void SetLocalY(this Transform t, float y)
	{
	  Vector3 p = new Vector3(t.localPosition.x, y, t.localPosition.z); 
	  t.localPosition = p;
	}
	
	public static void SetLocalZ(this Transform t, float z)
	{
	  Vector3 p = new Vector3(t.localPosition.x, t.localPosition.y, z); 
	  t.localPosition = p;
	}
	
	
	//what
	public static void SetPosition(this Transform t, float x, float y, float z)
	{
	  Vector3 p = new Vector3(x,y,z); 
	  t.position = p;
	}
	public static void SetPosition(this Transform t, Vector3 pos)
	{
	  t.position = pos;
	}
	
	public static void SetLocalPosition(this Transform t, Vector3 pos)
	{
	  t.localPosition = pos;
	}
	
	public static void CopyFrom(this Transform dst, Transform src)
	{
	  dst.localEulerAngles = src.localEulerAngles;
	  dst.localPosition = src.localPosition;
	  dst.localRotation = src.localRotation;
	  dst.localScale = src.localScale;
	}
	
	public static void MoveLocalUp(this Transform tr, float off)
	{
	  tr.SetLocalPosition( tr.localPosition + tr.up * off );
	}
	
	public static void LerpPosition(this Transform tr, Vector3 pos, float lerp)
	{
	  Vector3 p = new Vector3( Mathf.Lerp(tr.position.x, pos.x, lerp), 
	                     Mathf.Lerp(tr.position.y, pos.y, lerp),
	                     Mathf.Lerp(tr.position.z, pos.z, lerp) );
	  tr.position = p;
	} 
	
	public static void Draw(this Transform t, float size)
	{
	  Gizmos.color = Color.red;
	  Gizmos.DrawLine(t.position, t.position + t.right * size);
	 
	  Gizmos.color = Color.green;
	  Gizmos.DrawLine(t.position, t.position + t.up * size);
	 
	  Gizmos.color = Color.blue;
	  Gizmos.DrawLine(t.position, t.position + t.forward * size); 
	}
	
	public static void Set(this Transform t, Vector3 pos, Quaternion rot)
	{
	  t.position = pos;
	  t.rotation = rot;
	} 
}
