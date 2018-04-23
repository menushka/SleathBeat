using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	public void GoToLevelSelect() {
		SceneManager.LoadScene("LevelSelect");
	}

	public void Quit() {
		Application.Quit();
	}
}
