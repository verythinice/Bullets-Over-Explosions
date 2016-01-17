using UnityEngine;
using System.Collections;

public class ExplodingObjectController : MonoBehaviour {
    public float explosionRadius = 3f;

    GameObject explosionSystemPrefab;
    GameObject explosionTriggerPrefab;

    void Awake()
    {
        explosionSystemPrefab = Resources.Load("Prefabs/ExplosionSystemBlue") as GameObject;
        explosionTriggerPrefab = Resources.Load("Prefabs/ExplosionTrigger") as GameObject;
    }

	void LateUpdate(){
		if (this.tag == "Explode") {


            Instantiate(explosionSystemPrefab, this.transform.position, Quaternion.identity);
            this.tag = "Untagged";
            GameObject explosion = (GameObject)Instantiate(explosionTriggerPrefab, this.transform.position, Quaternion.identity);
            explosion.GetComponent<ExplosionTriggerController>().maxRadius = explosionRadius;
			Destroy (this.gameObject);
		}
	}
}
