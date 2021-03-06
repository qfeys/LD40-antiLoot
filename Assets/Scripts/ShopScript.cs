﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopScript : MonoBehaviour {

    public float value;
    
    // OnTriggerEnter2D is called when the Collider2D other enters the trigger (2D physics only)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Player>() != null)
        {
            UI_shop.inventory = GenerateNewLoot();
            UI_Stats.SwitchWindowStance(UI_Stats.WindowStance.shop);
            Destroy(gameObject);
            MusicPlayer.PlayBel();
        }
    }

    private List<Loot> GenerateNewLoot()
    {
        List<Loot> ret = new List<Loot>();
        while (value > 1 && ret.Count < 10)
        {
            ret.Add(Loot.GetRandLoot(Random.Range(value * .3f, value * .9f)));
            value -= ret.FindLast(l => true).value;
        }
        return ret;
    }
}
