using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject playerGameObject;
	public Vector3 offset;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!playerGameObject) return;

		Vector3 oldPosition = transform.position;
		Vector3 newPosition = playerGameObject.transform.position + offset;
		transform.position = Vector3.Lerp(oldPosition, newPosition, 0.1f);
	}
}
