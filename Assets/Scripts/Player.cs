﻿using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    public int hitpoints = 10;
    public int coins = 0;
    public List<Loot> inventory;
    public Equipment equipment = new Equipment();
    public GameOver gameOver;

    public static Player instance;
    public int xp;
    public float carryingCap = 10;
    public float TotalWeight { get { return inventory.Sum(l => l.weight) + coins / 10; } }
    internal static float encumburance = 1;
    private float doge = 0;

    private void Awake()
    {
        if (instance != null) throw new Exception("2nd instance of player");
        instance = this;
        inventory = new List<Loot>();
    }

    private void Start()
    {
        equipment.rHand.item = new Loot.Melee(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update ()
    {
        bool validTerrain = MapGenerator.instance.IsValidTerrain(transform.position);
        bool shieldUp = ((equipment.lHand.item != null) && Input.GetMouseButton(1));
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * speed * encumburance * Time.deltaTime * (validTerrain ? 1 : .4f) * (shieldUp ? .4f : 1), Space.World);
        }
        if (Input.GetButton("Vertical"))
        {
            transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * speed * encumburance * Time.deltaTime * (validTerrain ? 1 : .4f) * (shieldUp ? .4f : 1), Space.World);
        }

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);

    }

    internal void Attack(Enemy enemy, int damage)
    {
        enemy.ProcessHit(damage);
    }

    internal void ProcessHit(Enemy enemy)
    {
        if (UnityEngine.Random.value < doge) {
            Debug.Log("Doged the attack");
            return;
        }
        float armor = equipment.Armor();
        if(UnityEngine.Random.value < armor)
        {
            Debug.Log("Deflected the attack");
            return;
        }
        hitpoints -= enemy.damage;
        Debug.Log("Hit. HP left: " + hitpoints);
        if(hitpoints <= 0)
        {
            gameOver.distance = transform.position.magnitude;
            UI_Stats.SwitchWindowStance(UI_Stats.WindowStance.gameOver);
        }
    }

    public class Equipment
    {
        public ItemSlot head = new ItemSlot(Loot.ItemSlot.head);
        public ItemSlot chest = new ItemSlot(Loot.ItemSlot.chest);
        public ItemSlot rHand = new ItemSlot(Loot.ItemSlot.rightHand);
        public ItemSlot rArm = new ItemSlot(Loot.ItemSlot.arm);
        public ItemSlot lHand = new ItemSlot(Loot.ItemSlot.leftHand);
        public ItemSlot lArm = new ItemSlot(Loot.ItemSlot.arm);
        public ItemSlot rLeg = new ItemSlot(Loot.ItemSlot.leg);
        public ItemSlot lLeg = new ItemSlot(Loot.ItemSlot.leg);

        internal float Armor()
        {
            bool shieldActive = Input.GetMouseButton(1);
            float a = 0;
            a += (head.item == null ? 0 : ((head.item) as Loot.Armor).blockChance);
            a += (chest.item == null ? 0 : ((chest.item) as Loot.Armor).blockChance);
            a += (rArm.item == null ? 0 : ((rArm.item) as Loot.Armor).blockChance);
            a += (lArm.item == null ? 0 : ((lArm.item) as Loot.Armor).blockChance);
            a += (rLeg.item == null ? 0 : ((rLeg.item) as Loot.Armor).blockChance);
            a += (lLeg.item == null ? 0 : ((lLeg.item) as Loot.Armor).blockChance);
            a += (lHand.item == null ? 0 : shieldActive ? ((lHand.item) as Loot.Shield).blockChanceActive : ((lHand.item) as Loot.Shield).blockChancePassive);
            return a;
        }
    }

    public class ItemSlot
    {
        public Loot.ItemSlot slot;
        public Loot item;

        public ItemSlot(Loot.ItemSlot slot)
        {
            this.slot = slot;
        }

        public string GetItemName()
        {
            if (item == null)
                return "--";
            else return item.ToString();
        }

        public string GetItemStats()
        {
            if (item == null)
                return null;
            else return item.GetStats();
        }
    }

}
