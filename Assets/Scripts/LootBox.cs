using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBox : MonoBehaviour {

    Vector2 pos;
    float lifetime;
    float rps = .5f;

    int coins;
    Loot loot;


	// Use this for initialization
	void Start () {
        coins = Random.Range(1, 6);
        pos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (Vector3)pos + Quaternion.Euler(0, 0, lifetime * 360 * rps) * Vector3.up * .1f;
        lifetime += Time.deltaTime;
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.coins += coins;
            Player.instance.inventory.Add(loot);
            Destroy(gameObject);
        }
    }

}
