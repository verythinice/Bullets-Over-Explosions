using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {


	void LateUpdate () {
		if (this.tag == "Dead") {
			Destroy (this.gameObject);
		}
	}
}
