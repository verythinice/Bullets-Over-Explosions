using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {
    public enum circleStart { left, right, top, down}

	public float moveSpeed;
	public float radius;
	public Transform prefab;
	public bool circleMove;
    public circleStart rotateStart = circleStart.right;
	public GameObject centerPoint;

	float timeCounter = 0;

	void Update(){
		
		if (circleMove == true) {
			CircleRotate();
		}
	}

	void LateUpdate () {
		if (this.tag == "Dead") {
            Invoke("Death", 0f);
		}
	}

    void CircleRotate()
    {
        timeCounter += Time.deltaTime;
        float x = 0, y = 0;
        switch (rotateStart)
        {
            case circleStart.right:
                x = Mathf.Cos (timeCounter*moveSpeed);
		        y = Mathf.Sin (timeCounter*moveSpeed);
                break;
            case circleStart.left:
                x = Mathf.Cos((timeCounter * moveSpeed) + Mathf.PI);
                y = Mathf.Sin((timeCounter * moveSpeed) + Mathf.PI);
                break;
            case circleStart.top:
                x = Mathf.Cos((timeCounter * moveSpeed) + (Mathf.PI)/2);
                y = Mathf.Sin((timeCounter * moveSpeed) + (Mathf.PI)/2);
                break;
            case circleStart.down:
                x = Mathf.Cos((timeCounter * moveSpeed) + 3*(Mathf.PI)/2);
                y = Mathf.Sin((timeCounter * moveSpeed) + 3*(Mathf.PI)/2);
                break;
        }
        	
		transform.position = new Vector2 (centerPoint.transform.position.x + (x * radius), centerPoint.transform.position.y + (y * radius));
    }

    void Death()
    {
        Instantiate(prefab, this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
