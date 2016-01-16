using UnityEngine;
using System.Collections;

public class ExplodingObjectController : MonoBehaviour {
	public Transform prefab;

	void LateUpdate(){
		if (this.tag == "explode") {
			Instantiate (prefab, this.transform.position, Quaternion.identity);
		}
	}
}
