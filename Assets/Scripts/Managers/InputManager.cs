using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour {

	public const int FORWARD = 0;
	public const int BACK = 1;
	public const int LEFT = 2;
	public const int RIGHT = 3;

	public delegate void OnInputPressed(int input);
	public static OnInputPressed OnInput;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (OnInput != null) {
			if (Input.GetKeyDown(KeyCode.W)) OnInput(FORWARD);
			if (Input.GetKeyDown(KeyCode.S)) OnInput(BACK);
			if (Input.GetKeyDown(KeyCode.A)) OnInput(LEFT);
			if (Input.GetKeyDown(KeyCode.D)) OnInput(RIGHT);
		}
	}
}
