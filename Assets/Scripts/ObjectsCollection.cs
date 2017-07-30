using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsCollection : MonoBehaviour {

	public GameObject[] allObjects;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void addObject(GameObject g)
	{
		if (allObjects.Length == 0) {
			allObjects = new GameObject[1];
			allObjects[0] = g;
		} else {
			GameObject[] tempObjects = allObjects;
			allObjects = new GameObject[tempObjects.Length + 1];

			for (int i = 0; i < tempObjects.Length; i++) {
				allObjects [i] = tempObjects [i];

			}

			allObjects [allObjects.Length - 1] = g;
		}
	}
}
