using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SpriteMovement : NetworkBehaviour {


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	/*void Update () {
		if (transform.parent.GetComponent<PlayerMovement> ().WalkDir != 0) {
			time = time + Time.deltaTime;
			float phase = Mathf.Sin (time / period);
			transform.localRotation = Quaternion.Euler( new Vector3 (0,0,phase*angle));
		}
	}*/




}
