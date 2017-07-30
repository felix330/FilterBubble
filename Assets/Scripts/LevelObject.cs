using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class LevelObject : MonoBehaviour {

	public int shownToPlayer;

	public Sprite p1Sprite;
	public Sprite p2Sprite;
	public Sprite P0Sprite;

	public GameObject localVersion;
	// Use this for initialization
	void Start () {
		transform.parent.GetComponent<ObjectsCollection> ().addObject (gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void setSprite(int i) {

	}

}
