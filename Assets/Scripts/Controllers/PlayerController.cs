using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float noise = 0;

	private int x;
	private int z;
	private GameObject model;

	// Use this for initialization
	void Start () {
		x = StageManager.instance.currentStage.start[0];
		z = StageManager.instance.currentStage.start[1];
		model = transform.GetChild(0).gameObject;

		InputManager.OnInput = OnInput;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 oldPosition = transform.position;
		Vector3 newPosition = new Vector3(x + 0.5f, 0.45f, z + 0.5f);
		transform.position = Vector3.Lerp(oldPosition, newPosition, 0.5f);
	}

	void OnInput(int control) {
		int xmove = 0;
		int zmove = 0;
		switch(control) {
			case InputManager.FORWARD:
				zmove += -1;
				model.transform.rotation = Quaternion.Euler(0, -180, 0);
				break;
			case InputManager.BACK:
				zmove += 1;
				model.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case InputManager.LEFT:
				xmove += 1;
				model.transform.rotation = Quaternion.Euler(0, 90, 0);
				break;
			case InputManager.RIGHT:
				xmove += -1;
				model.transform.rotation = Quaternion.Euler(0, -90, 0);
				break;
		}

		int result = StageManager.instance.currentStage.at(x + xmove, z + zmove);
		if (result == 0) {
			x += xmove;
			z += zmove;
		}

		if (!MusicManager.instance.isDownBeat()) {
			ChangeNoise(0.1f);
		}
	}

	void ChangeNoise(float value) {
		noise += value;
		noise = Mathf.Clamp01(noise);
		UIManager.instance.setNoise(noise);
	}
}
