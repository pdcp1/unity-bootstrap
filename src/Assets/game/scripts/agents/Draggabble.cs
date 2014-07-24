using UnityEngine;
using System.Collections;

public class Draggabble : MonoBehaviour 
{
	public event System.Action<Draggabble> OnStartDrag;
	public event System.Action<Draggabble, Vector2> OnDrag;
	public event System.Action<Draggabble> OnReleaseDrag;
	public event System.Action<Draggabble> OnClick;
	tk2dUIItem uiItem;


	public tk2dUIItem UiItem
	{ 
		get
		{
			if(uiItem == null)
			{
				uiItem =GetComponent<tk2dUIItem>();
			}
			return uiItem;
		}
	}

	bool dragging = false;
	
	public bool Dragging
	{
		get { return dragging;}
		protected set 
		{ 
			dragging = value; 
		}
	}
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnEnable()
	{
		UiItem.OnDown += onDown;
		UiItem.OnRelease += onUp; 
	}
	
	void OnDisable()
	{
		UiItem.OnDown -= onDown;
		UiItem.OnRelease -= onUp;
	}
	
	protected void onDown()
	{		
		Down();
	}
	
	protected void onUp()
	{		
		Up();
	}
	
	Vector3 startDragPosotion = Vector3.zero;
	
	public void Down()
	{
		tk2dUIManager.Instance.OnInputUpdate += UpdateBtnPosition;
		offset = CalculateOffset();
		startDragPosotion = transform.position;
		//GameManager.Instance.AudioManager.PlayFX(GameManager.FX_BUTTON_CLICK);
	}
	
	public void Up()
	{
		tk2dUIManager.Instance.OnInputUpdate -= UpdateBtnPosition;
		startDragPosotion = Vector2.zero;

		if(Dragging)
		{
			Dragging = false;

			if(OnReleaseDrag != null)
				OnReleaseDrag(this);
		}
		else
		{
			if(OnClick != null)
				OnClick(this);
		}

	}

	private void UpdateBtnPosition()
	{
		var newPosition = CalculateNewPos();
		var bias = startDragPosotion - newPosition;
		
		if(bias.sqrMagnitude > 0.005f && !Dragging)
		{
			if( Mathf.Abs(bias.x) > Mathf.Abs(bias.y))
			{
				Debug.Log("horizontal bias");
			}
			else
			{
				Debug.Log("vertical bias");
			}
			
			if(OnStartDrag != null)
				OnStartDrag(this);

			Dragging = true;
		}
		
		if(Dragging)
		{
			if(OnDrag != null)
				OnDrag(this, newPosition);
		}
	}

	private Vector3 offset = Vector3.zero; //offset on touch/click
	
	
	private Vector3 CalculateOffset()
	{
		Vector2 pos = UiItem.Touch.position;
		
		Camera viewingCamera = tk2dUIManager.Instance.GetUICameraForControl( gameObject );
		Vector3 worldPos = viewingCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, transform.position.z - viewingCamera.transform.position.z));
		worldPos.z = transform.position.z;
		
		return transform.position - worldPos;
	}
	
	private Vector3 CalculateNewPos()
	{
		Vector2 pos = UiItem.Touch.position;
		
		Camera viewingCamera = tk2dUIManager.Instance.GetUICameraForControl( gameObject );
		Vector3 worldPos = viewingCamera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, transform.position.z - viewingCamera.transform.position.z));
		worldPos.z = transform.position.z;
		worldPos += offset;
		return worldPos;
	}
}
