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
	}
	
	// Update is called once per frame
	void Update () {
        transform.position = (Vector3)pos + Quaternion.Euler(0, 0, lifetime * 360 * rps) * Vector3.up * .1f;
        lifetime += Time.deltaTime;
	}

    public void SetLoot(float value)
    {
        coins = (int)Random.Range(value / 10, value - 1) + 1;
        pos = transform.position;
        if (Random.value > 0.6)
            loot = Loot.GetRandLoot(value * 1.2f - coins);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Player.instance.coins += coins;
            if (loot != null)
            {
                Player.instance.inventory.Add(loot);
            }
            Destroy(gameObject);
        }
    }

}
