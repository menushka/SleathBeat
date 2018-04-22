using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PulsingText : MonoBehaviour {

	bool down = true;
	float opacity = 1f;
	Text text;

	// Use this for initialization
	void Start () {
		text = GetComponent<Text>();
		StartCoroutine(Pulse());
	}

	IEnumerator Pulse() {
		while(true) {
			if (down) {
				opacity -= 0.1f;
			} else {
				opacity += 0.1f;
			}
			if (opacity <= 0) {
				down = false;
			} 
			
			if (opacity >= 1) {
				down = true;
			}
			Color c = text.color;
			c.a = opacity;
			text.color = c;
			yield return new WaitForSeconds(0.1f);
		}
	}
}
