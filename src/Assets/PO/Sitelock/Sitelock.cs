using UnityEngine;
using System.Collections;

public class Sitelock : MonoBehaviour {
	
	public string LocktoSite;
	
	
#if UNITY_WEBPLAYER && !UNITY_EDITOR
	// Use this for initialization
	void Start () 
	{	
		var js = @"

			console.log(parent, parent.document.location.host, parent.document.location.host.match('{0}') );

		";
		
		var test = 
			@"
				if(parent && parent.document.location.host != 'localhost' && ! parent.document.location.host.match('{0}') )
				{
					document.location='http://goo.gl/9ulDD';
				}
			";
		
			Application.ExternalEval(string.Format(js, LocktoSite));
			Application.ExternalEval(string.Format(test, LocktoSite));

	}
	
#endif 

}
