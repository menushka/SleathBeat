using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#pragma warning disable 0436
public class PlayerController : MonoBehaviour {

	public static PlayerController instance;

	public int x;
	public int z;
	public float noise = 0;

	public bool dead = false;
	public bool win = false;
	private GameObject model;
	private GameObject noiseView;

	private Object explosionObject;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}

		explosionObject = Resources.Load("Prefabs/Explosion", typeof(GameObject));
	}

	public void Kill() {
		if (dead) return;
		dead = true;
		Instantiate(explosionObject, transform);
		Destroy(model);
	}

	// Use this for initialization
	void Start () {
		x = StageManager.instance.currentStage.start[0];
		z = StageManager.instance.currentStage.start[1];
		model = transform.GetChild(0).gameObject;
		noiseView = transform.GetChild(1).gameObject;

		StartCoroutine(QuietDown());

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
				if (dead || win) return;
				zmove += -1;
				model.transform.rotation = Quaternion.Euler(0, -180, 0);
				break;
			case InputManager.BACK:
				if (dead || win) return;
				zmove += 1;
				model.transform.rotation = Quaternion.Euler(0, 0, 0);
				break;
			case InputManager.LEFT:
				if (dead || win) return;
				xmove += 1;
				model.transform.rotation = Quaternion.Euler(0, 90, 0);
				break;
			case InputManager.RIGHT:
				if (dead || win) return;
				xmove += -1;
				model.transform.rotation = Quaternion.Euler(0, -90, 0);
				break;
			case InputManager.ESC:
				SceneManager.LoadScene("LevelSelect");
				break;
			case InputManager.ANY:
				if (dead || win) {
					SceneManager.LoadScene("Game");
				}
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

		int[] w = StageManager.instance.currentStage.end;
		if (x == w[0] && z == w[1]) {
			win = true;
		}
	}

	void ChangeNoise(float value) {
		noise += value;
		noise = Mathf.Clamp01(noise);
		noiseView.transform.localScale = new Vector3(noise * 10, 0.01f, noise * 10);

		// Notify Enemies
		if (EnemyController.allEnemies != null) {
			foreach (EnemyController enemy in EnemyController.allEnemies) {
				enemy.ListenFor(this);
			}
		}
	}

	IEnumerator QuietDown() {
		while(true) {
			if (noise > 0) {
				ChangeNoise(-0.1f);
			}
			yield return new WaitForSeconds(0.5f);
		}
	}
}
