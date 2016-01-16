using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
    
    public float moveSpeed;

    LineRenderer sight;    
    RaycastHit2D[] hitList;

    void Awake()
    {
        sight = this.transform.FindChild("Sight").GetComponent<LineRenderer>();
    }
		
	void Update () {
        mouseLook();

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
        }        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            Shoot();            
        }               
    }

    private void Shoot()
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

    private void mouseLook()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);        
        Vector3 dir = mousePos - this.transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, new Vector3(0, 0, 1));
        
        Vector3 lineEndPos = new Vector3(dir.x * 100, dir.y * 100, 0);
        hitList = Physics2D.RaycastAll(this.transform.position, mousePos - this.transform.position);
        foreach (RaycastHit2D hit in hitList)
        {
            if (hit.collider.CompareTag("Wall"))
            {
                Debug.Log(hit.point);
                lineEndPos = hit.point;
            }
        }
        sight.SetPosition(0, this.transform.position);
        sight.SetPosition(1, lineEndPos);
    }
}
