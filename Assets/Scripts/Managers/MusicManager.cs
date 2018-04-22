﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0436
public class MusicManager : MonoBehaviour {

	public static MusicManager instance;

	public AudioSource audioSource;
	public Image test;

	public float bps = 120f / 60f;
	[HideInInspector]
	private int[] beat = {1,0,0,0, 1,0,1,0, 1,0,0,0, 1,1,1,1,
						 1,0,0,0, 1,0,1,0, 1,0,0,0, 0,0,0,0,
						 1,0,0,0, 1,0,1,0, 1,0,0,0, 1,1,1,1,
						 1,0,0,0, 1,0,1,0, 1,0,0,0, 0,0,0,0};
	// public int[] beat = {1,0,0,0, 1,0,0,0, 1,0,0,0, 1,0,0,0,
	// 					 1,0,0,0, 1,0,0,0, 1,0,0,0, 1,0,0,0,
	// 					 1,0,0,0, 1,0,0,0, 1,0,0,0, 1,0,0,0,
	// 					 1,0,0,0, 1,0,0,0, 1,0,0,0, 1,0,0,0};
	private int currentBeat = 0;

	public int[] GetBeat() {
		return beat;
	}

	public float GetSeconds() {
		return (float) audioSource.timeSamples / (float) audioSource.clip.frequency;
	}

	public bool isDownBeat(bool debug = false) {
		float seconds = GetSeconds();
		float beats = seconds * bps * 4;
		int currentBeat = Mathf.RoundToInt(beats) - 1;
		currentBeat = Mathf.Clamp(currentBeat, 0, 63);
		if (beat[currentBeat] == 1) {
			return true;
		} else {
			return false;
		}
	}

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
		if (isDownBeat()) {
			test.color = Color.red;
		} else {
			test.color = Color.white;
		}
	}

	private bool WithinRange(float value, int target, float difference) {
		if (value > target - difference && value < target + difference) {
			return true;
		} else {
			return false;
		}
	}
}
