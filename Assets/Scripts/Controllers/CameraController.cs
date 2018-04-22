using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public GameObject playerGameObject;
	public Vector3 offset;
	public float size;

	public Vector3 centerTarget = new Vector3(-1, -1, -1);
	private Vector3 centerOffset = new Vector3(3, 5, 3);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 oldPosition = transform.position;
		if (centerTarget != new Vector3(-1, -1, -1)) {
			Vector3 newPosition = centerTarget + centerOffset;
			transform.position = Vector3.Lerp(oldPosition, newPosition, 0.1f);
		} else if (playerGameObject != null) {
			Vector3 newPosition = playerGameObject.transform.position + offset;
			transform.position = Vector3.Lerp(oldPosition, newPosition, 0.1f);	
		}
		float currentSize = Camera.main.orthographicSize;
		Camera.main.orthographicSize = Mathf.Lerp(currentSize, size, 0.5f);
	}
}
