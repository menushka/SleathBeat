using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteGroupController : MonoBehaviour {

	RectTransform rt;

	void Start() {
		rt = GetComponent<RectTransform>();
	}

	void Update() {
		float time = MusicManager.instance.GetSeconds();
		rt.anchoredPosition = new Vector3(-time * MusicManager.instance.GetBPS() * 4 * 15, 10, 0);
	}
}
