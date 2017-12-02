using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 5f;
    public int hitpoints = 5;
    public GameObject slashGraphic;
    int weaponDamage = 1;
    public int coins = 0;
    public List<Loot> inventory;
    public Equipment equipment;

    public static Player instance;
    public int xp;

    private void Awake()
    {
        if (instance != null) throw new Exception("2nd instance of player");
        instance = this;
        inventory = new List<Loot>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.Translate(Vector3.right * Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, Space.World);
        }
        if (Input.GetButton("Vertical"))
        {
            transform.Translate(Vector3.up * Input.GetAxisRaw("Vertical") * speed * Time.deltaTime, Space.World);
        }

        Vector3 screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z - Camera.main.transform.position.z));
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2((screenPos.y - transform.position.y), (screenPos.x - transform.position.x)) * Mathf.Rad2Deg);

        if (Input.GetMouseButtonDown(0))
        {
            slashGraphic.SetActive(true);
            Invoke("DeactivateSlash", .2f);
        }
    }

    internal void Attack(Enemy enemy)
    {
        enemy.hitpoints -= weaponDamage;
        if (enemy.hitpoints <= 0)
            Destroy(enemy.gameObject);
    }

    internal void Hit(Enemy enemy)
    {
        hitpoints -= enemy.damage;
        Debug.Log("Hit. HP left: " + hitpoints);
    }

    void DeactivateSlash()
    {
        slashGraphic.SetActive(false);
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
    }

    public class ItemSlot
    {
        public Loot.ItemSlot slot;
        public Loot item;

        public ItemSlot(Loot.ItemSlot slot)
        {
            this.slot = slot;
        }
    }

}
