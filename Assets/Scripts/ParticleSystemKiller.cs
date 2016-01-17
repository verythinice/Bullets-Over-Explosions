using UnityEngine;
using System.Collections;

public class ParticleSystemKiller : MonoBehaviour {
    private float maxTime;
    private float time;

	// Use this for initialization
	void Start () {
        var systems = GetComponentsInChildren<ParticleSystem>();
        maxTime = 0;
        time = 0;
        foreach (ParticleSystem system in systems)
        {
            if (system.startLifetime> maxTime)
            {
                maxTime = system.startLifetime;
            }
        }
        Camera.main.GetComponent<CameraShakeScript>().screenShake(0.7f, 0.5f);
    }
	
	// Update is called once per frame
	void FixedUpdate () {
	    if (time>= maxTime)
        {
            Destroy(this.gameObject);
        }
        else
        {
            time += Time.deltaTime;
        }
	}
}
