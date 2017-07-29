using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMaster : MonoBehaviour {

	public GameObject testObject;
	public bool spawnNow;
	public bool connect;

	public GameObject player1;
	public GameObject player2;

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
