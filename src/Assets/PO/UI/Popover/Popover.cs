using UnityEngine;
using System.Collections;

public enum PopoverPlacement
{
	TOP_RIGHT,
	TOP_LEFT
}
	
public class Popover : ComponentSingleton<Popover> 
{
		
	public tk2dSprite arrow;
	public tk2dTextMesh text;
	public tk2dSlicedSprite bg;
	
	public string Text;
	public int Width;
	public PopoverPlacement Placement;
	public Vector2 Position;
	
	void Start()
	{
		InitSingleton();
		Hide();
	}
	
	public void Show()
	{
		Refresh();	
		gameObject.SetActive(true);
	}
	
	public void Hide()
	{
		gameObject.SetActive(false);
	}
	
	
	public void Refresh()
	{
		//	
		bg.dimensions = new Vector2(Width, bg.dimensions.y);

		//
		switch(Placement)
		{
		case PopoverPlacement.TOP_RIGHT:
			
				arrow.transform.SetLocalX( Width -16);	
				transform.position = new Vector3(Position.x - bg.dimensions.x, Position.y + bg.dimensions.y - 12, transform.position.z);	
			break;
		case PopoverPlacement.TOP_LEFT:
			
				Debug.LogError("nor suppported placement");
			
			break;
		}
		
		//
		text.text = Text;
		text.wordWrapWidth =  (int)bg.dimensions.x * 30;
		text.Commit();
	}
	
#if UNITY_EDITOR
	void Update () 
	{
		Refresh();
	}
#endif
	
	
}
