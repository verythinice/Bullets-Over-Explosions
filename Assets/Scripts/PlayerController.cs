﻿using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

    public enum bulletEnum { piercing, bouncing, exploding }
    
    public float moveSpeed;
    public bulletEnum currentBullet;
    
    LvlMngrController levelManager;
    PlayerBulletAPI bulletAPI;
    AudioPlayer audioPlayer;
    LineRenderer sight;   
    Rigidbody2D rigidBody;    
    Vector3 mousePos;   

    void Awake()
    {
        levelManager = GameObject.Find("LevelManager").GetComponent<LvlMngrController>();
        bulletAPI = GetComponent<PlayerBulletAPI>();
        audioPlayer = GetComponent<AudioPlayer>();
        rigidBody = GetComponent<Rigidbody2D>();
        sight = this.transform.FindChild("Sight").GetComponent<LineRenderer>();               
    }
		
	void Update () {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal, vertical);

        rigidBody.AddForce(moveSpeed * movement / Time.deltaTime);

        mouseLook();

        if (Input.GetKeyDown(KeyCode.Alpha1) && levelManager.currentPierceAmmo > 0)
        {
            currentBullet = bulletEnum.piercing;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && levelManager.currentBounceAmmo > 0)
        {
            currentBullet = bulletEnum.bouncing;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && levelManager.currentExplosionAmmo > 0)
        {
            currentBullet = bulletEnum.exploding;
        }

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
                if (levelManager.currentPierceAmmo > 0)
                {
                    audioPlayer.Play("PierceLaser");
                    bulletAPI.PiercingShot(mousePos);
                    levelManager.currentPierceAmmo--;
                }                
                break;
            case bulletEnum.bouncing:
                if (levelManager.currentBounceAmmo > 0)
                {
                    audioPlayer.Play("BounceLaser");
                    bulletAPI.BouncingShot(mousePos);
                    levelManager.currentBounceAmmo--;
                }                
                break;
            case bulletEnum.exploding:
                if (levelManager.currentExplosionAmmo > 0)
                {
                    audioPlayer.Play("ExplosiveLaser");
                    bulletAPI.ExplodingShot(mousePos);
                    levelManager.currentExplosionAmmo--;
                }                
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

        Vector3 initialPos = this.transform.position;
        Vector3 targetPos = mousePos - initialPos;
        sight.SetPosition(0, initialPos);        

        for (int i = 1; i <= 2; i++)
        {            
            targetPos = targetPos.normalized * 100;
            Vector3 vertexPos = targetPos;            

            initialPos += (targetPos).normalized * 0.1f;            
            RaycastHit2D[] hitList = Physics2D.RaycastAll(initialPos, targetPos);

            foreach (RaycastHit2D hit in hitList)
            {
                if (hit.collider.CompareTag("Wall"))  
                {                                   
                    vertexPos = hit.point;
                    initialPos = hit.point;
                    targetPos = Vector2.Reflect(targetPos, hit.normal);                    
                    break;
                }               
            }
            
            sight.SetPosition(i, vertexPos);
            if (currentBullet != bulletEnum.bouncing)
            {
                sight.SetPosition(2, vertexPos);
                break;
            }                
        }
       

        //Vector3 linePos1 = dir.normalized * 100;

        //Vector3 linePos2 = linePos1;

        //RaycastHit2D[] hitList = Physics2D.RaycastAll(this.transform.position, mousePos - this.transform.position);
        //foreach (RaycastHit2D hit in hitList)
        //{
        //    if (hit.collider.CompareTag("Wall"))
        //    {     
        //        linePos1 = hit.point;
        //        if (currentBullet == bulletEnum.bouncing)
        //        {                                    
        //            linePos2 = Vector2.Reflect(linePos2, hit.normal);                    
        //        }
        //        break;
        //    }
        //}
        //sight.SetPosition(0, this.transform.position);
        //sight.SetPosition(1, linePos1);

        //linePos2 = linePos1 + (2 * linePos2.normalized);
        //sight.SetPosition(2, linePos2);
    }
}
