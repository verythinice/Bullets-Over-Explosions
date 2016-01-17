using UnityEngine;
using System.Collections;

public class ExplodingObjectController : MonoBehaviour {
	public Transform selfExplode;
	public Transform triggerExplode;
	public float delay;
	void LateUpdate(){
		if (this.tag == "Explode") {
			

			Instantiate (selfExplode, this.transform.position, Quaternion.identity);
			this.tag = "Untagged";
			Invoke ("delayExplosion", delay);
			//Destroy (this.gameObject);
		}
	}
	void delayExplosion(){
		Instantiate (triggerExplode, this.transform.position, Quaternion.identity);
		Destroy (this.gameObject);
	}
}
