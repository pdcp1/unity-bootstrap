using UnityEngine;
using System.Collections;

public class Basket : MonoBehaviour {

	Draggabble draggable;

	// Use this for initialization
	void Start () {
		draggable = GetComponent<Draggabble>();

		draggable.OnDrag += HandleOnDrag;
	}

	void HandleOnDrag (Draggabble draggable, Vector2 position)
	{
		draggable.transform.position = new Vector2(position.x, transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
