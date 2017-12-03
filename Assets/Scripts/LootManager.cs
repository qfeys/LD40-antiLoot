using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public GameObject LootBoxPrefab;
    static public LootManager instance;

	// Use this for initialization
	void Start ()
    {
        if (instance != null) throw new Exception("2nd instance of lootmanager");
        instance = this;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceLootbox(Vector2 pos, float value)
    {
        GameObject lb = GameObject.Instantiate(LootBoxPrefab, pos, Quaternion.identity);
        lb.GetComponent<LootBox>().SetLoot(value);
    }
}
