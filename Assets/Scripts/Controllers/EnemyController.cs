using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {

	public static List<EnemyController> allEnemies;

	private readonly int[] STAY  = new int[]{0,0};
	private readonly int[] UP    = new int[]{-1,0};
	private readonly int[] RIGHT = new int[]{0,1};
	private readonly int[] DOWN  = new int[]{1,0};
	private readonly int[] LEFT  = new int[]{0,-1};

	private bool searching = false;
	private Coroutine stateBehaviour;

	private Enemy enemyCache;
	private int x;
	private int z;
	private string move;
	private int[] look;
	private int lookDistance = 3;

	void Awake() {
		if (allEnemies == null) {
			allEnemies = new List<EnemyController>();
		} 
		allEnemies.Add(this);
	}

	void Start () {
		stateBehaviour = StartCoroutine(Move());
	}
	
	void Update () {
		Vector3 oldPosition = transform.position;
		Vector3 newPosition = new Vector3(x + 0.5f, 0.45f, z + 0.5f);
		transform.position = Vector3.Lerp(oldPosition, newPosition, 0.5f);

		PlayerController pc = PlayerController.instance;
		if (pc == null) return; 
		if (look == UP) {
			if (z == pc.z && x > pc.x && !StageManager.instance.currentStage.obstacleBetween(x, z, pc.x, pc.z)) 
				Kill();
		} else if (look == DOWN) {
			if (z == pc.z && x < pc.x && !StageManager.instance.currentStage.obstacleBetween(x, z, pc.x, pc.z)) 
				Kill();
		} else if (look == LEFT) {
			if (x == pc.x && z > pc.z && !StageManager.instance.currentStage.obstacleBetween(x, z, pc.x, pc.z)) 
				Kill();
		} else if (look == RIGHT) {
			if (x == pc.x && z < pc.z && !StageManager.instance.currentStage.obstacleBetween(x, z, pc.x, pc.z)) 
				Kill();
		}
	}

	public void Kill() {
		if (stateBehaviour != null) StopCoroutine(stateBehaviour);
		PlayerController pc = PlayerController.instance;
		Vector3 centerPos = new Vector3((x + pc.x) / 2, 0, (z + pc.z) / 2);
		Camera.main.GetComponent<CameraController>().centerTarget = centerPos;
		Camera.main.GetComponent<CameraController>().size = 3;
		PlayerController.instance.Kill();
	}

	public void Init(Enemy enemy) {
		this.enemyCache = enemy;
		this.x = enemy.z;
		this.z = enemy.x;
		this.move = enemy.move;
	}

	public void ListenFor(PlayerController player) {
		if (searching) return;
		float phpx = player.transform.localPosition.x;
		float phpz = player.transform.localPosition.z;
		float phx = transform.localPosition.x;
		float phz = transform.localPosition.z;
		float distance = Mathf.Sqrt(Mathf.Pow(Mathf.Abs(phpx - phx), 2) + Mathf.Pow(Mathf.Abs(phpz - phz), 2));
		if (distance < player.noise * 5) {
			searching = true;
			if (stateBehaviour != null) StopCoroutine(stateBehaviour);
			stateBehaviour = StartCoroutine(Search());
		}
	}

	private int GetMove(int index) {
		string s = move.Substring(index, 1);
		return int.Parse(s);
	}

	private int GetMove(int index, string customPath) {
		string s = customPath.Substring(index, 1);
		return int.Parse(s);
	}

	IEnumerator Move() {
		int i = 0;
		while(true) {
			int[][] possibleMove = {STAY, UP, RIGHT, DOWN, LEFT};
			int index = GetMove(i);
			int[] selected = possibleMove[index];
			transform.rotation = Quaternion.Euler(0, 180 + 90 * index, 0);
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

	IEnumerator Search() {
		int[] end = { PlayerController.instance.x, PlayerController.instance.z };
		string path = CalculateAStar(end);
		for (int i = 0; i < path.Length; i++) {
			int[][] possibleMove = {STAY, UP, RIGHT, DOWN, LEFT};
			int index = GetMove(i, path);
			int[] selected = possibleMove[index];
			transform.rotation = Quaternion.Euler(0, 180 + 90 * index, 0);
			if (selected != STAY) look = selected; 
			int result = StageManager.instance.currentStage.at(x + selected[0], z + selected[1]);
			if (result == 0) {
				x += selected[0];
				z += selected[1];
			}
			yield return new WaitForSeconds(0.5f);
		}
		if (stateBehaviour != null) StopCoroutine(stateBehaviour);
		stateBehaviour = StartCoroutine(ReturnTo());
	}

	IEnumerator ReturnTo() {
		int[] end = { enemyCache.z, enemyCache.x };
		string path = CalculateAStar(end);
		for (int i = 0; i < path.Length; i++) {
			int[][] possibleMove = {STAY, UP, RIGHT, DOWN, LEFT};
			int index = GetMove(i, path);
			int[] selected = possibleMove[index];
			transform.rotation = Quaternion.Euler(0, 180 + 90 * index, 0);
			if (selected != STAY) look = selected; 
			int result = StageManager.instance.currentStage.at(x + selected[0], z + selected[1]);
			if (result == 0) {
				x += selected[0];
				z += selected[1];
			}
			yield return new WaitForSeconds(0.5f);
		}
		searching = false;
		if (stateBehaviour != null) StopCoroutine(stateBehaviour);
		stateBehaviour = StartCoroutine(Move());
	}

	string CalculateAStar(int[] end) {
		int[] start = { x, z };
		List<Node> nodes = new List<Node>();
		nodes.Add(new Node(start, end, null, -1));

		// while(true) {
		for (int i = 0; i < 1000; i++) {
			nodes.Sort((n1, n2) => n1.distance.CompareTo(n2.distance));
			Node n = nodes[0];
			if (n.distance == 0) {
				break;
			}
			for (int j = 0; j < 4; j++) {
				int[] dir = new int[][]{UP, RIGHT, DOWN, LEFT}[j];
				int[] newPos = new int[]{ n.x + dir[0], n.z + dir[1] };
				int bType = StageManager.instance.currentStage.at(newPos[0], newPos[1]);
				if (bType == 0) {
					nodes.Add(new Node(newPos, end, n, j));
				}
			}
			nodes.RemoveAt(0);
		}
		
		return GenerateMovePath(nodes[0]);
	}

	string GenerateMovePath(Node endNode) {
		string path = "";
		Node current = endNode;
		for (int i = 0; i < 1000; i++) {
			path = current.move + path;

			if (current.parent == null) break;
			current = current.parent;
		}
		return path;
	}

	void OnDrawGizmos() {
        // Gizmos.color = Color.white;
		// Gizmos.DrawLine(transform.position, transform.position + new Vector3(look[0] * lookDistance, 0, look[1] * lookDistance));
    }
}

public class Node {
	public int x;
	public int z;
	public float distance;
	public Node parent;
	public string move;

	public Node(int[] pos, int[] end, Node parent, int move) {
		this.x = pos[0];
		this.z = pos[1];
		this.distance = Mathf.Sqrt(Mathf.Pow(end[0] - pos[0], 2) + Mathf.Pow(end[1] - pos[1], 2));
		this.parent = parent;
		switch(move) {
			case 0:
				this.move = "1";
				break;
			case 1:
				this.move = "2";
				break;
			case 2:
				this.move = "3";
				break;
			case 3:
				this.move = "4";
				break;
		}
	}

	public override string ToString() {
		return x + " : " + z + " : " + distance;
	}
}
