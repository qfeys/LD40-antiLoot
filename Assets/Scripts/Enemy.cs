using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 4;
    public int hitpoints = 1;
    public int damage = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        transform.rotation = Quaternion.Euler(0, 0, 
            Mathf.Atan2((Player.instance.transform.position.y - transform.position.y), (Player.instance.transform.position.x - transform.position.x)) * Mathf.Rad2Deg);
        transform.Translate(Vector3.right * speed * Time.deltaTime);
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Player pl = collision.GetComponentInParent<Player>();
            pl.Hit(this);
        }
        if (collision.gameObject.CompareTag("Weapon"))
        {
            Player pl = collision.GetComponentInParent<Player>();
            pl.Attack(this);
        }
    }

    private void OnDestroy()
    {
        LootManager.instance.PlaceLootbox(transform.position);
    }


}
