using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashScript : MonoBehaviour {

    public WeaponScript ws;

    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
            if (collision.GetComponentInParent<Enemy>() != null)
            {
                ws.Attack(collision.GetComponentInParent<Enemy>());
            }
    }
}
