using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0436
public class MusicManager : MonoBehaviour {

	public static MusicManager instance;

	public AudioSource audioSource;
	public Image test;

	float bpm = 120f;
	float bps = 120f / 60f;
	int[,] beat = {{1,0,0,0}, {1,0,1,0}, {1,0,0,0}, {1,1,1,1},
				   {1,0,0,0}, {1,0,1,0}, {1,0,0,0}, {0,0,0,0},
				   {1,0,0,0}, {1,0,1,0}, {1,0,0,0}, {1,1,1,1},
				   {1,0,0,0}, {1,0,1,0}, {1,0,0,0}, {0,0,0,0}};
	int currentBeat = 0;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

    void Start () {
		audioSource.Play();
	}
	
	void Update () {
		float seconds = (float) audioSource.timeSamples / (float) audioSource.clip.frequency;
		int currentBeat = Mathf.FloorToInt(seconds * bps * 4);
		currentBeat = Mathf.Clamp(currentBeat, 0, 63);
		if (beat[currentBeat / 4, currentBeat % 4] == 1) {
			test.color = Color.red;
		} else {
			test.color = Color.white;
		}
	}
}
