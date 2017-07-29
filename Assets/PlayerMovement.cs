using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public GameObject child;
	public GameObject objects;

	// Use this for initialization
	void Start () {
		objects = GameObject.Find ("Objects");
	}

	public override void OnStartLocalPlayer() {
		child.GetComponent<SpriteRenderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButton ("Jump")) {
			Debug.Log ("Jump");
			CmdChangeItem ();
			//objects.BroadcastMessage ("ReceiveMessage");
		}

		if (!isLocalPlayer)
			return;



		if (Input.GetAxis ("Horizontal") != 0) {
			transform.position = new Vector2 (transform.position.x + Input.GetAxis ("Horizontal"), transform.position.y);
		}
	}

	[Command]
	void CmdChangeItem ()
	{
		objects.transform.position = new Vector2 (0, 1);

	}
}
