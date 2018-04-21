using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#pragma warning disable 0436
public class StageManager : MonoBehaviour {

	public static StageManager instance;

	void Awake() {
		if (instance == null) {
			instance = this;
		} else {
			Destroy(this);
		}
	}

	void Start () {
		
	}

	void Update () {
		
	}
}
