using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour {

    public WeaponScript ws;
    public float speed;
    public int damage;
    float lifeTime = 1;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        transform.position += (transform.right.normalized * Time.deltaTime * speed);
        lifeTime -= Time.deltaTime;
        if (lifeTime < 0)
            Destroy(gameObject);
	}

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Enemy>() != null)
        {
            ws.Attack(collision.GetComponentInParent<Enemy>(), damage);
        }
    }
}
