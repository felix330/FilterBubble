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

	public float angle;
	public float period;

	private float time;

	public Sprite p1Sprite;
	public Sprite p2Sprite;

	private bool rotateRight;

	[SyncVar(hook = "CmdChangeSwitch")]
	public bool readyToSwitch;

	[SyncVar(hook = "CmdChangeWalkDir")]
	public int WalkDir;

	[SyncVar(hook = "CmdChangeChildAngle")]
		public Vector3 ChildVector; 

	public LayerMask p1Mask;
	public LayerMask p2Mask;

	public GameObject questionMark;



	// Use this for initialization
	void Start () {
		gamemaster = GameObject.Find ("GameMaster");
		objects = GameObject.Find ("Objects");
		Debug.Log (GetInstanceID());



		CmdSpawnChild();

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
		//child.GetComponent<SpriteRenderer>().material.color = Color.green;
	}
	
	// Update is called once per frame
	void Update () {



		//RpcSwitch ();
		
		if (!isLocalPlayer)
			return;

		if (Input.GetButtonDown ("Fire2")) {
			Debug.Log ("Getting ready");
			if (readyToSwitch) {
				CmdChangeSwitch (false);
			} else {
				CmdChangeSwitch (true);
			}
		}

		CharacterController controller = GetComponent<CharacterController>();
		if (Input.GetAxis ("Horizontal") < 0) {
			
			CmdChangeWalkDir (1);
		} else if (Input.GetAxis ("Horizontal") > 0) {
			CmdChangeWalkDir (2);

		} else {
			CmdChangeWalkDir (0);
		}
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
	void CmdChangeWalkDir(int i) {
		WalkDir = i;
	}

	[Command]
	void CmdSpawnChild() {
	}

	[Command]
	void CmdWalkWiggle() {
		if (rotateRight) {
			Debug.Log ("Rotating Right");
			if (child.transform.localEulerAngles.z < 20) {
				child.transform.localEulerAngles += new Vector3 (0,0,50*Time.deltaTime);
			} else {
				rotateRight = false;
			}
		} else {
			if (child.transform.localEulerAngles.z > -20) {
				Debug.Log ("Rotating left");
				child.transform.localEulerAngles -= new Vector3 (0,0,50*Time.deltaTime);
			} else {
				Debug.Log ("Reset");
				rotateRight = true;
			}
		}
	}

	[ClientRpc]
	public void RpcUpdate()
	{
		if (WalkDir != 0) {
			time = time + Time.deltaTime;
			float phase = Mathf.Sin (time / period);
			Vector3 newRot = new Vector3 (0, 0, phase * angle);
			CmdChangeChildAngle (newRot);
			child.transform.localRotation = Quaternion.Euler(ChildVector);
		}
	}

	[Command] 
	public void CmdChangeChildAngle(Vector3 v) {
		ChildVector = v;
		child.transform.localRotation = Quaternion.Euler(ChildVector);
	}

	[ClientRpc]
	public void RpcChangeWalkDir (int i)
	{
		if (i == 1) {
			child.transform.localScale = new Vector3 (-1.36f, child.transform.localScale.y, child.transform.localScale.z);
		} else if (i == 2) {
			child.transform.localScale = new Vector3 (1.36f, child.transform.localScale.y, child.transform.localScale.z);
		}
	}

	[ClientRpc]
	public void RpcChangeChildLayer (int i)
	{
		child.layer = i;
	}

	[ClientRpc]
	public void RpcChangeLayer(int i)
	{
		if (i == 1) {
			myCamera.GetComponent<Camera> ().cullingMask = p1Mask;
		} else if (i == 2) {
			myCamera.GetComponent<Camera> ().cullingMask = p2Mask;
		}
	}

	[ClientRpc]
	public void RpcChangeSprite(int i)
	{
		if (i == 1) {
			child.GetComponent<SpriteRenderer> ().sprite = p1Sprite;
		} else if (i == 2) {
			child.GetComponent<SpriteRenderer> ().sprite = p2Sprite;
		}
	}

	[Client]
	public void RpcSwitch(){
		Debug.Log ("Switch");
		/*if (gamemaster.GetComponent<GameMaster> ().player1 == gameObject) {
			myCamera.GetComponent<Camera> ().cullingMask = p1Mask;
			Debug.Log ("Switching to P1");
		} else if (gamemaster.GetComponent<GameMaster> ().player2 == gameObject) {
			myCamera.GetComponent<Camera> ().cullingMask = p2Mask;
			Debug.Log ("Switching to P2");
		} else {
			Debug.Log("Error, there is nothing to Switch");
		}*/
		CmdChangeSwitch (false);
	}

	[Command]
	void CmdChangeSwitch(bool b)
	{
		questionMark.GetComponent<SpriteRenderer> ().enabled = b;
		readyToSwitch = b;
	}

	[ClientRpc]
	public void RpcSetVisibility(bool b)
	{
		questionMark.GetComponent<SpriteRenderer> ().enabled = b;
	}

	[Command]
	void CmdSendToGM () {
		gamemaster.GetComponent<GameMaster>().addPlayer(gameObject);
	}

	[Command]
	void CmdChangeItem ()
	{
		//objects.transform.position = new Vector2 (0, 1);
	}
}
