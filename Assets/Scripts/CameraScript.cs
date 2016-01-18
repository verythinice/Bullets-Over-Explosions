using UnityEngine;
using System.Collections;
using System;

public class CameraScript : MonoBehaviour {
    public float xBuffer = 10f;
    public float yBuffer = 10f;
    private GameObject player;
    private PlayerController controller;

	// Use this for initialization
	void Start () {
        player = GameObject.Find("Player");
        controller = player.GetComponent<PlayerController>();
	}

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 playerPosition = player.transform.position;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseTarget = new Vector3();
        Vector3 targetDirection = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        if (Math.Abs(playerPosition.x - transform.position.x) > xBuffer || Math.Abs(mousePos.x - transform.position.x) > xBuffer) {
            if (Math.Abs(playerPosition.x - mousePos.x) > 2 * xBuffer)
            {
                if (playerPosition.x > mousePos.x)
                {
                    mouseTarget.x = playerPosition.x - 2*xBuffer;
                }
                else
                {
                    mouseTarget.x = playerPosition.x + 2*xBuffer;
                }
            }
            else
            {
                mouseTarget.x = mousePos.x;
            }
            targetDirection.x = (playerPosition.x + mouseTarget.x) / 2;
        }
        if (Math.Abs(playerPosition.y - transform.position.y) > yBuffer || Math.Abs(mousePos.y - transform.position.y) > yBuffer)
        {
            if (Math.Abs(playerPosition.y - mousePos.y) > 2 * yBuffer)
            {
                if (playerPosition.y > mousePos.y)
                {
                    mouseTarget.y = playerPosition.y - 2*yBuffer;
                }
                else
                {
                    mouseTarget.y = playerPosition.y + 2*yBuffer;
                }
            }
            else
            {
                mouseTarget.y = mousePos.y;
            }
            targetDirection.y = (playerPosition.y + mouseTarget.y) / 2;
        }
        transform.position = Vector2.MoveTowards(transform.position, targetDirection, controller.moveSpeed * Time.deltaTime);
    }

    //COME ON AND SLAM
    //shakeAmount is how big the shake is
    //shakeTime is how long to shake
    public void CameraShake(float shakeAmount, float shakeTime)
    {
        GetComponentInChildren<CameraShakeScript>().screenShake(shakeAmount, shakeTime);
    }
}
