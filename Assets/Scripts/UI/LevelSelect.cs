using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#pragma warning disable 0436
public class LevelSelect : MonoBehaviour {

	public static string selectedLevel;

	public Button level1;
	public Button level2;
	public Button level3;

	void Start() {
		level1.onClick.AddListener(OnLevel1Click);
		level2.onClick.AddListener(OnLevel2Click);
		level3.onClick.AddListener(OnLevel3Click);
	}

	void OnLevel1Click() {
		LevelSelect.selectedLevel = "tutorial";
		GoToGame();
	}

	void OnLevel2Click() {
		LevelSelect.selectedLevel = "level1";
		GoToGame();
	}

	void OnLevel3Click() {
		LevelSelect.selectedLevel = "level2";
		GoToGame();
	}

	public void GoToGame() {
		SceneManager.LoadScene("Game");
	}
}
