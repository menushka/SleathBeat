using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0436
public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject canvasGameObject;

	private Text noiseText;

	private Object noteGroupObject;
	private Object noteObject;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}

		noiseText = canvasGameObject.GetComponentInChildren<Text>();

		noteGroupObject = Resources.Load("Prefabs/Note Group", typeof(GameObject));
		noteObject = Resources.Load("Prefabs/Note", typeof(GameObject));
	}

	void Start () {
		SpawnNotes();
	}
	
	void Update () {
		
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

	public void setNoise(float noise) {
		noiseText.text = "Noise Level: " + noise;
	}
}
