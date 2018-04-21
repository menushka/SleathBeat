using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		InputManager.OnInput = OnInput;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnInput(int control) {
		switch(control) {
			case InputManager.FORWARD:
				transform.position += new Vector3(0, 0, -1);
				break;
			case InputManager.BACK:
				transform.position += new Vector3(0, 0, 1);
				break;
			case InputManager.LEFT:
				transform.position += new Vector3(1, 0, 0);
				break;
			case InputManager.RIGHT:
				transform.position += new Vector3(-1, 0, 0);
				break;
		}
	}
}
