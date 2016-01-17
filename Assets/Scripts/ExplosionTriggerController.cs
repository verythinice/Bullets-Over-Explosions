using UnityEngine;
using System.Collections;

public class ExplosionTriggerController : MonoBehaviour {

    public float growthRate, maxRadius;

    void Start()
    {
        Invoke("CleanUp", 10f);
    }

    void Update()
    {

        if (this.GetComponent<CircleCollider2D>().radius < 2)
        {
            this.GetComponent<CircleCollider2D>().radius += growthRate * Time.deltaTime;
        }
    }

	void OnTriggerEnter2D(Collider2D other){		
		if(other.CompareTag("Enemy")){
			other.tag = "Dead";
		}
		if(other.CompareTag("Object")){
			other.tag = "Explode";
		}		
	}

    void CleanUp()
    {
        Destroy (this.gameObject);
    }
}
