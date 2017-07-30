using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerMovement : NetworkBehaviour {

	public GameObject child;
	public GameObject objects;
	private Vector2 moveDirection = Vector2.zero;
	public GameObject myCamera;
	public GameObject gamemaster;

	[SyncVar(hook = "CmdChangeSwitch")]
	public bool readyToSwitch;


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

		if (Input.GetButtonDown ("Fire2")) {
			Debug.Log ("Getting ready");
			if (readyToSwitch) {
				CmdChangeSwitch (false);

			} else {
				CmdChangeSwitch (true);
				transform.position = new Vector3 (0, 5, 0);
			}
		}

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
	public void CmdSwitch(){
		Debug.Log ("Switch");
		if (myCamera.GetComponent<Camera>().cullingMask == p2Mask) {
			myCamera.GetComponent<Camera> ().cullingMask = p1Mask;
			Debug.Log ("Switching to P1");
		} else if (myCamera.GetComponent<Camera>().cullingMask == p1Mask) {
			myCamera.GetComponent<Camera> ().cullingMask = p2Mask;
			Debug.Log ("Switching to P2");
		} else {
			Debug.Log("Error, there is nothing to Switch");
		}
		CmdChangeSwitch (false);
	}

	[Command]
	void CmdChangeSwitch(bool b)
	{
		readyToSwitch = b;
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
