using UnityEngine;
using System.Collections;

public class ExplosionTriggerController : MonoBehaviour {

    public float growthRate, maxRadius;

    LvlMngrController levelManager;

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LvlMngrController>();
    }

    void Start()
    {
        levelManager.currentExplosionTriggers++;        
    }

    void Update()
    {

        if (this.GetComponent<CircleCollider2D>().radius < 2)
        {
            this.GetComponent<CircleCollider2D>().radius += growthRate * Time.deltaTime;
        }
        else
        {
            levelManager.currentExplosionTriggers--;
            Destroy(this.gameObject);
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

    //void CleanUp()
    //{
    //    Destroy (this.gameObject);
    //}
}
