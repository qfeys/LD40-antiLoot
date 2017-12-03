using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    
    public GameObject enemy;
    static SpawnManager instance;
    
    void Awake ()
    {
        if (instance != null)
            throw new Exception("2x instances of Spawnmanager");
        instance = this;
    }

    internal static void SpawnEnemy(Vector2 pos, float speed, int hitpoints, int damage, float lungeRange, float attackSpeed, float value)
    {
        GameObject newEnemy = GameObject.Instantiate(instance.enemy, pos, Quaternion.identity);
        Enemy component = newEnemy.GetComponent<Enemy>();
        component.speed = speed;
        component.hitpoints = hitpoints;
        component.damage = damage;
        component.lungeRange = lungeRange;
        component.attackSpeed = attackSpeed;
        component.value = value;
        newEnemy.tag = "Enemy";
    }
}
