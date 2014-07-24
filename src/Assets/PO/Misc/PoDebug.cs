using UnityEngine;
using System.Collections;
 
public static class PoDebug
{
	public static void ForGizmo(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Gizmos.DrawRay(pos, direction);
 
		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
		Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
		Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
	}
 
	public static void ForGizmo(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Gizmos.color = color;
		Gizmos.DrawRay(pos, direction);
 
		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
		Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
		Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
	}
 
	public static void ForDebug(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Debug.DrawRay(pos, direction);
 
		Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180+arrowHeadAngle,0) * new Vector3(0,0,1);
		Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0,180-arrowHeadAngle,0) * new Vector3(0,0,1);
		Debug.DrawRay(pos + direction, right * arrowHeadLength);
		Debug.DrawRay(pos + direction, left * arrowHeadLength);
	}
	
	public static void ForDebug(Vector3 pos, Vector3 direction, Color color, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
	{
		Debug.DrawRay(pos, direction, color);
 
	    Vector3 up = Quaternion.LookRotation (direction) * Quaternion.Euler (0, arrowHeadAngle, 0) * Vector3.back;
        Vector3 down = Quaternion.LookRotation (direction) * Quaternion.Euler (0, -arrowHeadAngle, 0) * Vector3.back;
		Vector3 right = Quaternion.LookRotation (direction) * Quaternion.Euler (arrowHeadAngle, 0, 0) * Vector3.back;
        Vector3 left = Quaternion.LookRotation (direction) * Quaternion.Euler (-arrowHeadAngle, 0, 0) * Vector3.back;
		
       	Debug.DrawRay (pos + direction, right * arrowHeadLength, color);
        Debug.DrawRay (pos + direction, left * arrowHeadLength, color);
		Debug.DrawRay(pos + direction, up * arrowHeadLength, color);
		Debug.DrawRay(pos + direction, down * arrowHeadLength, color);
	}
	
	
	public static void DrawArrow(Vector3 from, Vector3 to, Color color)
	{
		PoDebug.ForDebug(from, to - from, color);
	}
	
}