using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class JoystickBase:MonoBehaviour 
{
    protected virtual void Awake ()
    {
		DontDestroyOnLoad(gameObject);
    }

	public virtual bool GetButton(JoystickButtons button)
	{
		return GetButton((int)button); 
	}

	public virtual bool GetButtonDown(JoystickButtons button)
	{
		return GetButtonDown((int)button);
	}

	public virtual bool GetButtonUp(JoystickButtons button)
	{
		return GetButtonUp((int)button);
	}

	public abstract bool GetButton(int button);
	public abstract bool GetButtonDown(int button);
	public abstract bool GetButtonUp(int button);

	public abstract bool IsActive();
	public abstract void Enable();
	public abstract void Disable();

	public abstract void Show(float duration);
	public abstract void Hide(float duration);


	public virtual event Action<JoystickButtons> OnButtonDown;
	public virtual event Action<JoystickButtons> OnButtonUp;

	public JoystickBase SetPlayer(int number)
	{
		currentPlayer = number;

		return this;
	}
	
	protected int currentPlayer = 1;
}


