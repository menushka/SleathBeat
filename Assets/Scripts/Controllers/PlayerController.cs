using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public int x = 0;
	public int z = 0;

	// Use this for initialization
	void Start () {
		InputManager.OnInput = OnInput;
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(x + 0.5f, 0.45f, z + 0.5f);
	}

	void OnInput(int control) {
		int xmove = 0;
		int zmove = 0;
		switch(control) {
			case InputManager.FORWARD:
				zmove += -1;
				break;
			case InputManager.BACK:
				zmove += 1;
				break;
			case InputManager.LEFT:
				xmove += 1;
				break;
			case InputManager.RIGHT:
				xmove += -1;
				break;
		}

		Debug.Log((x + xmove) + " : " + (z + zmove));
		int result = StageManager.instance.currentStage.at(x + xmove, z + zmove);
		Debug.Log(result);
		if (result == 0) {
			x += xmove;
			z += zmove;
		}
	}
}
