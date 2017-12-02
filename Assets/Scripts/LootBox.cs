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
        if (Random.value > 0.6)
            loot = Loot.GetRandLoot(20);
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
            if (loot != null)
            {
                Player.instance.inventory.Add(loot);
                Debug.Log("Loot: " + loot.ToString() + " , Damage: " + ((Loot.Melee)loot).damage + " , Range: " + ((Loot.Melee)loot).range + " , Weight: " + loot.weight + " , Value: " + loot.value);
            }
            Destroy(gameObject);
        }
    }

}
