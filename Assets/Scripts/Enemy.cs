﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 4;
    public int hitpoints = 1;
    public int damage = 1;
    public float lungeRange = .5f;
    public float attackSpeed = 1;
    public float value = 1;
    Collider2D myCollider;
    Collider2D playerCollider;
    float attackCooldown;

    float lifeTime = 60;

	// Use this for initialization
	void Start () {
        myCollider = GetComponentInChildren<CapsuleCollider2D>();
        playerCollider = Player.instance.GetComponentInChildren<CapsuleCollider2D>();
	}
	
	// Update is called once per frame
	void Update () {
        attackCooldown -= Time.deltaTime;
        if (attackCooldown > 0) return;

        transform.rotation = Quaternion.Euler(0, 0, 
            Mathf.Atan2((Player.instance.transform.position.y - transform.position.y), (Player.instance.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
        float distance = myCollider.Distance(playerCollider).distance;
        if(distance <= lungeRange)
        {
            attackCooldown = attackSpeed;
            transform.Translate(Vector3.right * lungeRange);
            Player.instance.ProcessHit(this);
            Invoke("FallBack", attackSpeed / 5);
            MusicPlayer.PlayBite();
        }
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
            Destroy(gameObject);
    }

    void FallBack()
    {
        transform.Translate(Vector3.left * lungeRange);
        Debug.Log("Fall Back");
    }

    internal void ProcessHit(int damage)
    {
        hitpoints -= damage;
        if (hitpoints <= 0)
            Die();
    }

    internal void Die()
    {
        LootManager.instance.PlaceLootbox(transform.position, value);
        Destroy(gameObject);
        MusicPlayer.PlayDeath();
    }
}
