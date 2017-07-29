﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public GameObject child;
	public GameObject objects;
	private Vector2 moveDirection = Vector2.zero;
	public GameObject myCamera;
	public GameObject gamemaster;

	public LayerMask p1Mask;
	public LayerMask p2Mask;

	// Use this for initialization
	void Start () {
		gamemaster = GameObject.Find ("GameMaster");
		objects = GameObject.Find ("Objects");
		Debug.Log (GetInstanceID());

		Debug.Log (gamemaster);
		CmdSendToGM ();

		if (!isLocalPlayer)
			myCamera.SetActive (false);
			return;

		myCamera.SetActive (true);

		if (gamemaster.GetComponent<GameMaster> ().player1 == gameObject) {
			myCamera.GetComponent<Camera> ().cullingMask = p1Mask;
		}  else if (gamemaster.GetComponent<GameMaster> ().player2 == gameObject) {
			myCamera.GetComponent<Camera> ().cullingMask = p2Mask;
		}
	}

	public override void OnStartLocalPlayer() {
		child.GetComponent<SpriteRenderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {
		if (!isLocalPlayer)
			return;

		CharacterController controller = GetComponent<CharacterController>();
		if (controller.isGrounded) {
			moveDirection = new Vector2 (Input.GetAxis ("Horizontal"), 0);
			moveDirection = transform.TransformDirection (moveDirection);
			moveDirection *= 6.0F;
			if (Input.GetButton ("Jump")) {
				moveDirection.y = 8.0F;
			}
		}
		moveDirection.y -= 20.0F * Time.deltaTime;
		controller.Move (moveDirection * Time.deltaTime);


		if (Input.GetButton ("Jump")) {
			Debug.Log ("Jump");
			CmdChangeItem ();
			//objects.BroadcastMessage ("ReceiveMessage");
		}

	}

	[Command]
	void CmdSendToGM () {
		gamemaster.GetComponent<GameMaster>().addPlayer(gameObject);
	}

	[Command]
	void CmdChangeItem ()
	{
		objects.transform.position = new Vector2 (0, 1);
	}
}
