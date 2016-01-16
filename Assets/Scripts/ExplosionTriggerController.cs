using UnityEngine;
using System.Collections;

public class ExplosionTriggerController : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D other){
		//Debug.Log (other.name);
		if(other.CompareTag("Enemy")){
			other.tag = "Dead";
		}
		if(other.CompareTag("Object")){
			other.tag = "Explode";
		}
		//Destroy (this.gameObject);
	}
}
