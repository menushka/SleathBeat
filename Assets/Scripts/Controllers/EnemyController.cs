using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	private readonly int[] STAY  = new int[]{0,0};
	private readonly int[] UP    = new int[]{-1,0};
	private readonly int[] RIGHT = new int[]{0,1};
	private readonly int[] DOWN  = new int[]{1,0};
	private readonly int[] LEFT  = new int[]{0,-1};

	private int x;
	private int z;
	private string move;
	private int[] look;
	private int lookDistance = 3;

	void Start () {
		StartCoroutine(Move());
	}
	
	void Update () {
		Vector3 oldPosition = transform.position;
		Vector3 newPosition = new Vector3(x + 0.5f, 0.75f, z + 0.5f);
		transform.position = Vector3.Lerp(oldPosition, newPosition, 0.5f);
	}

	public void Init(Enemy enemy) {
		this.x = enemy.z;
		this.z = enemy.x;
		this.move = enemy.move;
	}

	private int GetMove(int index) {
		string s = move.Substring(index, 1);
		return int.Parse(s);
	}

	IEnumerator Move() {
		int i = 0;
		while(true) {
			int[][] possibleMove = {STAY, UP, RIGHT, DOWN, LEFT};
			int[] selected = possibleMove[GetMove(i)];
			if (selected != STAY) look = selected; 
			int result = StageManager.instance.currentStage.at(x + selected[0], z + selected[1]);
			if (result == 0) {
				x += selected[0];
				z += selected[1];
			}
			yield return new WaitForSeconds(2f);
			i += 1;
			i %= move.Length;
		}
	}

	void OnDrawGizmos() {
        // Gizmos.color = Color.white;
		// Gizmos.DrawLine(transform.position, transform.position + new Vector3(look[0] * lookDistance, 0, look[1] * lookDistance));
    }
}
