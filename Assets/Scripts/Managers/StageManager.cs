﻿using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

#pragma warning disable 0436
public class StageManager : MonoBehaviour {

	public static StageManager instance;

	public GameObject stageGameObject;
	public Stage currentStage;

	private Object groundObject;
	private Object leftWallObject;
	private Object rightWallObject;
	private Object crateObject;
	private Object enemyObject;

	// TODO: Move to better location
	private Object playerObject;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}

		groundObject = Resources.Load("Prefabs/Ground", typeof(GameObject));
		leftWallObject = Resources.Load("Prefabs/Left Wall", typeof(GameObject));
		rightWallObject = Resources.Load("Prefabs/Right Wall", typeof(GameObject));
		crateObject = Resources.Load("Prefabs/Crate", typeof(GameObject));
		enemyObject = Resources.Load("Prefabs/Enemy", typeof(GameObject));

		playerObject = Resources.Load("Prefabs/Player", typeof(GameObject));
	}

	void Start() {
		LoadStageFile("level1");
		GenerateStage(currentStage);

		// Spawn player
		GameObject player = (GameObject) Instantiate(playerObject);
		Camera.main.GetComponent<CameraController>().playerGameObject = player;
	}

	void LoadStageFile(string fileName) {
		string filePath = Path.Combine(Application.streamingAssetsPath, fileName + ".json");
		if (File.Exists(filePath)) {
			string dataAsJson = File.ReadAllText(filePath);
            currentStage = JsonUtility.FromJson<Stage>(dataAsJson);
        } else {
            Debug.LogError(filePath + ": Stage not found.");
        } 
	}
	
	void GenerateStage(Stage stage) {
		int width = stage.map.Length;
		int length = stage.map[0].row.Length;

		GameObject ground = (GameObject) Instantiate(groundObject, stageGameObject.transform);
		ground.transform.localPosition = new Vector3(width / 2f, 0, length / 2f);
		ground.transform.localScale = new Vector3(width / 10f, 1, length / 10f);

		for (int y = 0; y < stage.map.Length; y++) {
			for (int x = 0; x < stage.map[y].row.Length; x++) {
				if (stage.at(x, y) == 1) {
					GameObject crate = (GameObject) Instantiate(crateObject, stageGameObject.transform);
					crate.transform.position = new Vector3(x + 0.5f, 0.5f, y + 0.5f);
				}
			}
		}

		foreach (Enemy enemy in stage.enemies) {
			GameObject enemyObj = (GameObject) Instantiate(enemyObject, stageGameObject.transform);
			enemyObj.GetComponent<EnemyController>().Init(enemy);
		}
	}
}

[System.Serializable]
public class Stage {
	public StageRow[] map;
	public Enemy[] enemies;
	public int[] start;

	public int at(int x, int y) {
		if (x < 0 || x >= map.Length) return 1;
		if (y < 0 || y >= map[x].row.Length) return 1; 
		return map[x].row[y];
	}
}

[System.Serializable]
public class StageRow {
	public int[] row;
}

[System.Serializable]
public class Enemy {
	public int x;
	public int z;
	public string move;
}
