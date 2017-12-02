using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour {

    Vector2 pos;
    float lifetime;
    float rps = .5f;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (Vector3)pos + Quaternion.Euler(0, 0, lifetime * 360 * rps) * Vector3.up * .1f;
        lifetime += Time.deltaTime;
	}
}
