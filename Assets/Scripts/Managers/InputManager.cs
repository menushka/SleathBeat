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
			if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow)) OnInput(FORWARD);
			if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow)) OnInput(BACK);
			if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) OnInput(LEFT);
			if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) OnInput(RIGHT);
		}
	}
}
