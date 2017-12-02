using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 4;
    public int hitpoints = 1;
    public int damage = 1;
    public float lungeRange = .5f;
    public float attackSpeed = 1;
    Collider2D myCollider;
    Collider2D playerCollider;
    float attackCooldown;

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
        }
    }

    void FallBack()
    {
        transform.Translate(Vector3.left * lungeRange);
        Debug.Log("Fall Back");
    }

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
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
