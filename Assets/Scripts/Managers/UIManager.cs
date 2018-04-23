using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0436
public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject canvasGameObject;

	private GameObject retryText;
	private GameObject levelCompleteText;
	private GameObject tutorial;

	private Object noteGroupObject;
	private Object noteObject;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}

		retryText = canvasGameObject.transform.Find("RetryText").gameObject;
		retryText.SetActive(false);

		levelCompleteText = canvasGameObject.transform.Find("LevelComplete").gameObject;
		levelCompleteText.SetActive(false);

		tutorial = canvasGameObject.transform.Find("Tutorial").gameObject;
		if (LevelSelect.selectedLevel != "tutorial") {
			tutorial.SetActive(false);
		}

		noteGroupObject = Resources.Load("Prefabs/Note Group", typeof(GameObject));
		noteObject = Resources.Load("Prefabs/Note", typeof(GameObject));
	}

	void Start () {
		SpawnNotes();
	}
	
	void Update () {
		if (PlayerController.instance.dead) {
			retryText.SetActive(true);
		} else {
			retryText.SetActive(false);
		}

		if (PlayerController.instance.win) {
			levelCompleteText.SetActive(true);
		} else {
			levelCompleteText.SetActive(false);
		}
	}

	public void SpawnNotes() {
		GameObject noteGroup = (GameObject) Instantiate(noteGroupObject);
		for (int x = 0; x < MusicManager.instance.GetBeat().Length; x++) {
			if (MusicManager.instance.GetBeat()[x] == 1) {
				GameObject note = (GameObject) Instantiate(noteObject);
				note.transform.SetParent(noteGroup.transform, false);
				note.GetComponent<RectTransform>().anchoredPosition = new Vector3(x * 15, 0, 0);
			}
		}
		noteGroup.transform.SetParent(canvasGameObject.transform, false);
	}

	public void ToggleTutorialScreen() {
		if (tutorial.GetComponent<RectTransform>().anchoredPosition.x == 200) {
			Vector3 p = tutorial.GetComponent<RectTransform>().anchoredPosition;
			p.x = -10;
			tutorial.GetComponent<RectTransform>().anchoredPosition = p;
		} else {
			Vector3 p = tutorial.GetComponent<RectTransform>().anchoredPosition;
			p.x = 200;
			tutorial.GetComponent<RectTransform>().anchoredPosition = p;
		}
	}
}
