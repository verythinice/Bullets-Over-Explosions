using UnityEngine;
using System.Collections;

public class LaserController : MonoBehaviour {
    public float lifetime = .25f;

	void Start () {
        Invoke("CleanUp", lifetime);
	}

    void CleanUp()
    {
        Destroy(this.gameObject);
    }
}
