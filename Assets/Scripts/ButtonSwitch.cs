using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ButtonSwitch : NetworkBehaviour {
	public GameObject trigger;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter () {
		//transform.Translate (Vector3.down);
		transform.localPosition = new Vector3 (0,-0.3F,0.1F);
		trigger.transform.Translate(new Vector3 (0, -3, 0));
		//trigger.transform.transl
	}
}
