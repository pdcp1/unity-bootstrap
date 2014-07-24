using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class CustomTrigger : MonoBehaviour 
{
	public Action<Collider> OnStay;
	public Action<Collider> OnEnter;
	public Action<Collider> OnExit;
	
	void OnTriggerStay(Collider other)
	{
		if(OnStay!= null)
		{
			OnStay(other);
		}
	}
	
	void OnTriggerEnter(Collider other)
	{
		if(OnEnter!= null)
		{
			OnEnter(other);
		}
	}
	
	void OnTriggerExit(Collider other)
	{
		if(OnExit!= null)
		{
			OnExit(other);
		}
	}
}
