using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerShowObjects : NetworkBehaviour {

	/*GameObject gamemaster;
	int showMode;



	// Use this for initialization
	void Start () {
		Debug.Log (GetInstanceID());
		gamemaster = GameObject.Find ("GameMaster");
		CmdSendToGM ();
		if (gamemaster.GetComponent<GameMaster> ().player1 == gameObject) {
			foreach (GameObject g in GetComponent<PlayerMovement>().objects.GetComponent<ObjectsCollection>().allObjects) {
				GameObject newG = Instantiate (g.GetComponent<LevelObject>().localVersion);
				newG.GetComponent<SpriteRenderer> ().sprite = g.GetComponent<LevelObject> ().p1Sprite;
			}
		}  else if (gamemaster.GetComponent<GameMaster> ().player2 == gameObject) {
			foreach (GameObject g in GetComponent<PlayerMovement>().objects.GetComponent<ObjectsCollection>().allObjects) {
				GameObject newG = Instantiate (g.GetComponent<LevelObject>().localVersion);
				newG.GetComponent<SpriteRenderer> ().sprite = g.GetComponent<LevelObject> ().p2Sprite;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	}

	[Command]
	void CmdSendToGM () {
		gamemaster.GetComponent<GameMaster> ().addPlayer (gameObject);
	}*/
}
