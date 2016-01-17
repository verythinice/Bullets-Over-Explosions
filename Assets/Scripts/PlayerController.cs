using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public enum bulletEnum { piercing, bouncing, exploding }
    
    public float moveSpeed;
    public bulletEnum currentBullet;

    PlayerBulletAPI bulletAPI;
    LineRenderer sight;   
    Rigidbody2D rigidBody;    
    Vector3 mousePos;   

    void Awake()
    {
        bulletAPI = GetComponent<PlayerBulletAPI>();
        rigidBody = GetComponent<Rigidbody2D>();
        sight = this.transform.FindChild("Sight").GetComponent<LineRenderer>();               
    }
		
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);

        rigidBody.AddForce(moveSpeed * movement / Time.deltaTime);

        mouseLook();

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();            
        }        
    }

    private void Shoot()
    {
        switch (currentBullet)
        {
            case bulletEnum.piercing:
                bulletAPI.PiercingShot(mousePos);                
                break;
            case bulletEnum.bouncing:                
                bulletAPI.BouncingShot(mousePos);
                break;
            case bulletEnum.exploding:
                //_ExplodingShot();
                break;
        }
    } 

    private void mouseLook()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos = (Vector3)(Vector2)mousePos;  //Changes mousePos.z from being -10 to correctly being 0
        Vector3 dir = mousePos - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, new Vector3(0, 0, 1));
        
        //Vector3 linePos1 = new Vector3(dir.x, dir.y, 0);
        //linePos1.Normalize();
        //linePos1 = Vector3.Scale(new Vector3(100, 100, 0), linePos1);
        Vector3 linePos1 = dir.normalized * 100;

        Vector3 linePos2 = linePos1;

        RaycastHit2D[] hitList = Physics2D.RaycastAll(this.transform.position, mousePos - this.transform.position);
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Wall"))
            {     
                linePos1 = hit.point;
                if (currentBullet == bulletEnum.bouncing)
                {                                    
                    linePos2 = Vector2.Reflect(linePos2, hit.normal);
                }
                break;
            }
        }
        sight.SetPosition(0, this.transform.position);
        sight.SetPosition(1, linePos1);
        sight.SetPosition(2, linePos2);
    }
}
