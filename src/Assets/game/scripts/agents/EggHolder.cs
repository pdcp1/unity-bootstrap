using UnityEngine;
using System.Collections;
using po.utils;

public class EggHolder : MonoBehaviour {

	public Egg EggPrefab;

	tk2dUIItem uiItem;
	tk2dTextMesh counter;

	int secondsLeft;
	int maxSeconds = 10;
	
	// Use this for initialization
	void Start () 
	{
		uiItem = GetComponentInChildren<tk2dUIItem>();
		counter = transform.FindChild("counter").GetComponent<tk2dTextMesh>();

		uiItem.OnUp += HandleOnDown;

		secondsLeft = 5 + Random.Range(1, 6);

		counter.SetText(secondsLeft.ToString());

		InvokeRepeating("Tick", 1, 1);
	}

	void HandleOnDown ()
	{
		CancelInvoke();

		secondsLeft ++;
		counter.SetText(secondsLeft.ToString());

		Check();
		InvokeRepeating("Tick", 1, 1);
	}

	void Tick()
	{
		secondsLeft --;
		counter.SetText(secondsLeft.ToString());
		Check();
	}


	void Check()
	{
		if(secondsLeft <= 0 || secondsLeft >= 10)
		{
			CancelInvoke();
			Instantiate(EggPrefab, transform.position, Quaternion.identity);
			Destroy(gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
