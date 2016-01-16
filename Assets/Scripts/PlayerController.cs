using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public enum bulletEnum { piercing, bouncing, exploding }

    public Vector3 normal;

    Rigidbody2D rigidBody;
    public float moveSpeed;
    public bulletEnum currentBullet;

    LineRenderer sight;    
    RaycastHit2D[] hitList;
    Vector3 mousePos;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    void Awake()
    {
        sight = this.transform.FindChild("Sight").GetComponent<LineRenderer>();
    }
		
	void Update () {
        /*        
	    if (Input.GetKey(KeyCode.W))
        {
            this.transform.Translate(new Vector2(0, 1) * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.S))
        {
            this.transform.Translate(new Vector2(0, -1) * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Translate(new Vector2(1, 0) * moveSpeed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Translate(new Vector2(-1, 0) * moveSpeed * Time.deltaTime, Space.World);            
        }*/

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
                _PiercingShot();
                break;
            case bulletEnum.bouncing:
                _BouncingShot();
                break;
            case bulletEnum.exploding:
                //_ExplodingShot();
                break;
        }
    }

    private void _PiercingShot()
    {
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                break;
            }
            if (hit.collider.CompareTag("Enemy"))
            {
                Destroy(hit.collider.gameObject);
            }
        }
    }

    private void _BouncingShot()
    {
        int breakCounter = 0;


        int bounceCount = 0;
        Vector3 initialPos = this.transform.position;
        Vector3 targetPos = mousePos - this.transform.position;

        while (bounceCount <= 5)
        {
            breakCounter++;
            if (breakCounter > 50)
            {
                break;
            }

            RaycastHit2D[] tempHitList = Physics2D.RaycastAll(initialPos, targetPos);
            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    bounceCount++;
                    targetPos = Vector3.Reflect(initialPos, hit.normal);
                    initialPos = hit.point;                    
                    break;
                }
                if (hit.collider.CompareTag("Enemy"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }           
        }        
    }

    private void mouseLook()
    {
        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        Vector3 dir = mousePos - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, new Vector3(0, 0, 1));
        
        Vector3 linePos1 = new Vector3(dir.x, dir.y, 0);
        linePos1.Normalize();
        linePos1 = Vector3.Scale(new Vector3(100, 100, 0), linePos1);

        Vector3 linePos2 = linePos1;

        hitList = Physics2D.RaycastAll(this.transform.position, mousePos - this.transform.position);
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Wall"))
            {     
                linePos1 = hit.point;
                if (currentBullet == bulletEnum.bouncing)
                {                
                    normal = hit.normal;
                    linePos2 = Vector2.Reflect(linePos2, hit.normal);
                }
            }
        }
        sight.SetPosition(0, this.transform.position);
        sight.SetPosition(1, linePos1);
        sight.SetPosition(2, linePos2);
    }
}
