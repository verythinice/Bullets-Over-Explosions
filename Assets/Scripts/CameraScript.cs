using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {
    public float xBuffer = 10f;
    public float yBuffer = 10f;
    private Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player").transform;
	}
	
	// Update is called once per frame
	void LateUpdate () {
        Vector3 playerPosition = player.transform.position;
        Vector3 targetDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Math.Abs(playerPosition.x - transform.position.x)>xBuffer)
        {
            if (playerPosition.x > transform.position.x)
            {
                targetDirection.x = playerPosition.x - xBuffer;
            }
            else
            {
                targetDirection.x = playerPosition.x + xBuffer;
            }
        }
        if (Math.Abs(playerPosition.y - transform.position.y) > yBuffer)
        {
            if (playerPosition.y > transform.position.y)
            {
                targetDirection.y = playerPosition.y - yBuffer;
            }
            else
            {
                targetDirection.y = playerPosition.y + yBuffer;
            }
        }
        transform.position = targetDirection;
    }

    //COME ON AND SLAM
    //shakeAmount is how big the shake is
    //shakeTime is how long to shake
    public void CameraShake(float shakeAmount, float shakeTime)
    {
        GetComponentInChildren<CameraShakeScript>().screenShake(shakeAmount, shakeTime);
    }
}
