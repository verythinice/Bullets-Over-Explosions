﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public Transform prefab;

	void LateUpdate () {
		if (this.tag == "Dead") {
			Instantiate (prefab, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
