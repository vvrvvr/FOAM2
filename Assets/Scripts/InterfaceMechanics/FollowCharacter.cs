using UnityEngine;
using System.Collections.Generic;
using System;


public class FollowCharacter : MonoBehaviour {


	public Transform MainTarget;
	private bool _isMainTargetNotNull;
	private float offset;


	private void Start()
	{
		_isMainTargetNotNull = MainTarget != null;
		offset = transform.position.y - MainTarget.transform.position.y;
		
	}

	void LateUpdate () 
	{
		if (_isMainTargetNotNull)
		{
			transform.position = new Vector3 (MainTarget.position.x,MainTarget.position.y + offset,MainTarget.position.z);
			transform.eulerAngles = new Vector3( transform.eulerAngles.x, MainTarget.eulerAngles.y, transform.eulerAngles.z );
	
		}
	}
}
