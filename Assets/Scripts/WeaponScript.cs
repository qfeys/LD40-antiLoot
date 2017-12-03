using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    Player pl;
    Loot weapon;
    bool isMelee;

	// Use this for initialization
	void Start () {
        pl = FindObjectOfType<Player>();
	}
	
	// Update is called once per frame
	void Update () {
        if (weapon != pl.equipment.rHand.item)
        {
            weapon = pl.equipment.rHand.item;
            isMelee = weapon is Loot.Melee;
            if (isMelee)
            {
                Loot.Melee melee = weapon as Loot.Melee;
                transform.localScale = new Vector3(melee.range, melee.range, 1);
            }
            
        }
	}

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (weapon != null && weapon is Loot.Melee)
        {
            if (collision.GetComponentInParent<Enemy>() != null)
            {
                pl.Attack(collision.GetComponentInParent<Enemy>(), (weapon as Loot.Melee).damage);
            }
        }
    }
}
