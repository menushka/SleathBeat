using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 0436
public class MusicManager : MonoBehaviour {

	public static MusicManager instance;

	public AudioSource audioSource;
	public AudioClip  audioClip;
	public Image test;

	public Music currentMusic;

	public int[] GetBeat() {
		return currentMusic.getBeat();
	}

	public float GetBPS() {
		return (float) currentMusic.bpm / 60f;
	}

	public float GetSeconds() {
		return (float) audioSource.timeSamples / (float) audioSource.clip.frequency;
	}

	public bool isDownBeat(bool debug = false) {
		float seconds = GetSeconds();
		float bps = GetBPS();
		float beats = seconds * bps * 4;
		int currentBeat = Mathf.RoundToInt(beats);
		currentBeat = Mathf.Clamp(currentBeat, 0, 63);
		if (currentMusic.getBeat()[currentBeat] == 1) {
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

		LoadMusicFile(StageManager.instance.currentStage.music);
	}

    void Start () {
		audioSource.clip = audioClip;
		audioSource.Play();
	}
	
	void Update () {
		if (isDownBeat()) {
			test.color = Color.red;
		} else {
			test.color = Color.white;
		}
	}

	void LoadMusicFile(string fileName) {
		string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
		if (File.Exists(filePath)) {
			string dataAsJson = File.ReadAllText(filePath);
            currentMusic = JsonUtility.FromJson<Music>(dataAsJson);
			Debug.Log(currentMusic.song);
			audioClip = Resources.Load(currentMusic.song, typeof(AudioClip)) as AudioClip;
        } else {
            Debug.LogError(filePath + ": Music not found.");
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

[System.Serializable]
public class Music {
	public string song;
	public int bpm;
	public string[] beats;

	private int[] cachedBeat = new int[]{};

	public int[] getBeat() {
		if (cachedBeat.Length > 0) {
			return cachedBeat;
		}

		string seq = "";
		foreach (string s in beats) {
			seq += s;
		}
		seq = seq.Replace(" ", "");
		char[] array = seq.ToCharArray();
		cachedBeat = Array.ConvertAll(array, c => (int) Char.GetNumericValue(c));
		return cachedBeat;
	}
}
