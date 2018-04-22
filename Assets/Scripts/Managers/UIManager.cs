using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0436
public class UIManager : MonoBehaviour {

	public static UIManager instance;

	public GameObject canvasGameObject;

	private Text noiseText;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}

		noiseText = canvasGameObject.GetComponentInChildren<Text>();
	}

	void Start () {

	}
	
	void Update () {
		
	}

	public void setNoise(float noise) {
		noiseText.text = "Noise Level: " + noise;
	}
}
