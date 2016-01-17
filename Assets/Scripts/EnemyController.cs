using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform prefab;
	public float moveSpeed;
	public bool circleMove;
	public GameObject centerPoint;

	float timeCounter = 0;

	void Update(){
		timeCounter += Time.deltaTime;
		if (circleMove == true) {
			float x = Mathf.Cos (timeCounter*moveSpeed);
			float y = Mathf.Sin (timeCounter*moveSpeed);
			//float z = 0;
			transform.position = new Vector2 (centerPoint.transform.position.x + x, centerPoint.transform.position.y + y);
		}
	}

	void LateUpdate () {
		if (this.tag == "Dead") {
			Instantiate (prefab, this.transform.position, Quaternion.identity);
            Destroy (this.gameObject);
		}
	}
}
