using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMaster : NetworkBehaviour {

	public GameObject testObject;
	public bool spawnNow;
	public bool connect;

	public bool playersVisible;

	[SyncVar]
	public GameObject player1;
	[SyncVar]
	public GameObject player2;

	public LayerMask p1Mask;
	public LayerMask p2Mask;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		

		if (spawnNow) {
			GameObject newObject = Instantiate (testObject);
			NetworkServer.Spawn (newObject);
			spawnNow = false;
			newObject.transform.parent = GameObject.Find ("Objects").transform;
		}

		if (player1 != null && player2 != null) {
			player1.GetComponent<PlayerMovement> ().RpcChangeSprite (1);
			player2.GetComponent<PlayerMovement> ().RpcChangeSprite (2);

			if (playersVisible) {
				player1.GetComponent<PlayerMovement> ().RpcChangeChildLayer (0);
				player2.GetComponent<PlayerMovement> ().RpcChangeChildLayer (0);
			} else {
				player1.GetComponent<PlayerMovement> ().RpcChangeChildLayer (8);
				player2.GetComponent<PlayerMovement> ().RpcChangeChildLayer (9);
			}
		}

		if (player1.GetComponent<PlayerMovement>().WalkDir == 0) {
		} else if (player1.GetComponent<PlayerMovement>().WalkDir == 1) {
			player1.GetComponent<PlayerMovement> ().RpcChangeWalkDir (1);
		} else if (player1.GetComponent<PlayerMovement>().WalkDir == 2) {
			player1.GetComponent<PlayerMovement> ().RpcChangeWalkDir (2);
		}

		if (player2.GetComponent<PlayerMovement>().WalkDir == 0) {
		} else if (player2.GetComponent<PlayerMovement>().WalkDir == 1) {
			player2.GetComponent<PlayerMovement> ().RpcChangeWalkDir (1);
		} else if (player2.GetComponent<PlayerMovement>().WalkDir == 2) {
			player2.GetComponent<PlayerMovement> ().RpcChangeWalkDir (2);
		}

		if (player1.GetComponent<PlayerMovement> ().readyToSwitch && player2.GetComponent<PlayerMovement> ().readyToSwitch) {
			GameObject playerTemp = player1;
			player1 = player2;
			player2 = playerTemp;

			Debug.Log ("Attempting Switch");

			player1.GetComponent<PlayerMovement> ().RpcSwitch ();
			player2.GetComponent<PlayerMovement> ().RpcSwitch ();

			//player1.GetComponent<PlayerMovement> ().myCamera.GetComponent<Camera> ().cullingMask = p1Mask;
			//player2.GetComponent<PlayerMovement> ().myCamera.GetComponent<Camera> ().cullingMask = p2Mask;

			//player1.GetComponent<PlayerMovement> ().RpcChangeLayer (1);
			//player2.GetComponent<PlayerMovement> ().RpcChangeLayer (2);
		}
		player1.GetComponent<PlayerMovement> ().RpcChangeLayer (1);
		player2.GetComponent<PlayerMovement> ().RpcChangeLayer (2);
	}

	public void addPlayer(GameObject g)
	{
		if (player1 == null) {
			player1 = g;
		} else if (player2 == null) {
			player2 = g;
		}
	}
}
