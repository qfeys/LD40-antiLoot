using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

    Player pl;
    public GameObject slashGraphic;
    public GameObject bowGraphic;
    public GameObject shieldGraphic;
    public GameObject arrowPrefab;
    Loot weapon;
    Loot.Shield shield;
    bool isMelee;

	// Use this for initialization
	void Start () {
        pl = FindObjectOfType<Player>();
        slashGraphic.GetComponent<SlashScript>().ws = this;
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
                slashGraphic.transform.localScale = new Vector3(melee.range, melee.range, 1);
                bowGraphic.SetActive(false);
            }
            else if(weapon != null)
            {
                bowGraphic.SetActive(true);
            }
        }
        if(shield != pl.equipment.lHand.item)
        {
            shield = pl.equipment.lHand.item as Loot.Shield;
            if (shield != null)
                shieldGraphic.SetActive(true);
            else
                shieldGraphic.SetActive(false);
        }


        if (isMelee && weapon != null)
        {
            if (Input.GetMouseButtonDown(0) && UI_Stats.windowstance == UI_Stats.WindowStance.non)
            {
                slashGraphic.SetActive(true);
                Invoke("DeactivateSlash", .2f);
                MusicPlayer.PlaySlash();
            }
        }else if (weapon != null) // Ranged
        {
            if (Input.GetMouseButtonDown(0) && UI_Stats.windowstance == UI_Stats.WindowStance.non)
            {
                GameObject newArrow = GameObject.Instantiate(arrowPrefab, transform.position, transform.rotation);
                ArrowScript arSc = newArrow.GetComponent<ArrowScript>();
                arSc.ws = this;
                arSc.speed = (weapon as Loot.Ranged).range / 1;
                arSc.damage = (weapon as Loot.Ranged).damage;
                MusicPlayer.PlayShot();
            }
        }

        if(shield != null)
        {
            if (Input.GetMouseButton(1))
                shieldGraphic.transform.localRotation = Quaternion.Euler(0, 0, 0);
            else
                shieldGraphic.transform.localRotation = Quaternion.Euler(0, 0, 35);
        }
    }

    void DeactivateSlash()
    {
        slashGraphic.SetActive(false);
    }

    internal void Attack(Enemy enemy)
    {
        if(isMelee)
            pl.Attack(enemy, (weapon as Loot.Melee).damage);
        else
            pl.Attack(enemy, (weapon as Loot.Ranged).damage);

    }

    internal void Attack(Enemy enemy, int damage)
    {
        pl.Attack(enemy, damage);
    }

}
