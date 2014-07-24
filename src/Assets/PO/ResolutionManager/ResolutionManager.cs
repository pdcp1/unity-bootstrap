using UnityEngine;
using System.Collections;

public class ResolutionManager:MonoBehaviour
{
	public Camera currentCamera
	{
		get; set;
	}

	public int ScreenToPixels
	{
		get { return 100;}	
	}

	//this values make sense only for an specific camera
	public float AspecRatio
	{
		get { return (float) Screen.width / (float) Screen.height; }
	}
	
	public float ScreenWidthInUnits
	{
		get { return ScreenHeightInUnits * AspecRatio; }
	}

	public float ScreenHeightInUnits
	{
		get { return currentCamera.orthographicSize * 2f;}
	}

	public Vector3 ScreenTopLeft
	{
		get { return currentCamera.ScreenToWorldPoint(new Vector3(0f, currentCamera.pixelHeight, 0f)); }
	}

	public Vector3 ScreenBottomLeft
	{
		get { return currentCamera.ScreenToWorldPoint(new Vector3(0f, 0f, 0f)); }
	}

	public Vector3 ScreenTopRight
	{
		get { return currentCamera.ScreenToWorldPoint(new Vector3( currentCamera.pixelWidth, currentCamera.pixelHeight, 0f)); }
	}

	public Vector3 ScreenBottomRight
	{
		get { return currentCamera.ScreenToWorldPoint(new Vector3( currentCamera.pixelWidth, 0f, 0f)); }
	}


	// Use this for initialization
	void Start () 
	{
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
