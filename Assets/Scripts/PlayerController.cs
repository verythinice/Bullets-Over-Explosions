using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if (Input.GetKey(KeyCode.W))
        {
            //moveUp();
        }
        if (Input.GetKey(KeyCode.S))
        {
            //moveDown();
        }
        if (Input.GetKey(KeyCode.A))
        {
            //moveLeft();
        }
        if (Input.GetKey(KeyCode.D))
        {
            //moveRight();
        }

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            //shoot();
            Debug.Log("Derp");
        }

        Debug.Log(Input.mousePosition);
    }
}
