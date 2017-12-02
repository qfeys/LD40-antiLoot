using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootManager : MonoBehaviour {

    public GameObject LootBoxPrefab;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void PlaceLootbox(Vector2 pos)
    {
        GameObject lb = GameObject.Instantiate(LootBoxPrefab, pos, Quaternion.identity);

    }
}
