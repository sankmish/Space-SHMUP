﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    [Header("Set in Inspector: Enemy")]
    public float speed = 10f;
    public float fireRate = 0.3f;
    public float health = 10;
    public int score = 100;

    protected SM_BoundsCheck bndCheck;

    void Awake()
    {
        bndCheck = GetComponent<SM_BoundsCheck>();
    }

    public Vector3 pos
    {
        get
        {
            return (this.transform.position);
        }
        set
        {
            this.transform.position = value;
        }
    }
	
	// Update is called once per frame
	void Update () {
        Move();

        if (bndCheck != null && !bndCheck.isOnScreen)
        {
            if (pos.y < -bndCheck.camHeight - bndCheck.radius) { 
                print("Destroyed");
            Destroy(gameObject);
         }
        }
	}

    public virtual void Move()
    {
        Vector3 tempPos = pos;
        tempPos.y -= speed * Time.deltaTime;
        pos = tempPos;
    }

    void OnCollisionEnter (Collision coll)
    {
        print("OnCollision");
        GameObject otherGO = coll.gameObject;
        switch (otherGO.tag)
        {
            case "ProjectileHero":
                print("Hit by projectie Hero");
                Projectile p = otherGO.GetComponent<Projectile>();
                if (!bndCheck.isOnScreen)
                {
                    Destroy(otherGO);
                    break;
                }

                health -= Main.GetWeaponDefinition(p.type).damageOnHit;
                if (health <= 0)
                {
                    Destroy(this.gameObject);
                }
                Destroy(otherGO);
                break;

            default:
                print("Enemy hit by non-ProjectileHero: " + otherGO.name);
                break;
        }
    }


}
