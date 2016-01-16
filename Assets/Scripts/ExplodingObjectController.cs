using UnityEngine;
using System.Collections;

public class ExplodingObjectController : MonoBehaviour {
	public Transform prefab1;
	public Transform prefab2;

	void LateUpdate(){
		if (this.tag == "Explode") {
			

			Instantiate (prefab1, this.transform.position, Quaternion.identity);
			Instantiate (prefab2, this.transform.position, Quaternion.identity);
			Destroy (this.gameObject);
		}
	}
}
