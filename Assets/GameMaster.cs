using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class GameMaster : MonoBehaviour {

	public GameObject testObject;
	public bool spawnNow;

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
}
