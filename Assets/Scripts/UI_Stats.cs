using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuintensUITools;

public class UI_Stats : MonoBehaviour {

    public Font DefaultFont;

    public enum WindowStance { non, inventory, shop}
    static WindowStance windowstance = WindowStance.non;

	// Use this for initialization
	void Start ()
    {
        QuintensUITools.Graphics.SetDefaultFont(DefaultFont);
        QuintensUITools.Graphics.SetPath(@"Assets\");
        QuintensUITools.Graphics.LoadGraphics();
        UI_inventory.Create(transform.parent.gameObject);
        UI_shop.Create(transform.parent.gameObject);

        CreateMe();

    }

    private void CreateMe()
    {
        TextBox xpBox1 = new TextBox(transform, TextRef.Create( "Xp", "Experiance", false), 28, TextAnchor.UpperLeft);
        xpBox1.transform.anchoredPosition = new Vector2(5, -4);
        TextBox xpBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.xp), 28, TextAnchor.UpperRight);
        xpBox2.transform.anchoredPosition = new Vector2(-5, -4);
        TextBox coinBox1 = new TextBox(transform, "Coins", 28, TextAnchor.UpperLeft);
        coinBox1.transform.anchoredPosition = new Vector2(5, -34);
        TextBox coinBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.coins), 28, TextAnchor.UpperRight);
        coinBox2.transform.anchoredPosition = new Vector2(-5, -34);
        TextBox weightBox1 = new TextBox(transform, "Weight", 28, TextAnchor.UpperLeft);
        weightBox1.transform.anchoredPosition = new Vector2(5, -64);
        TextBox weightBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.TotalWeight), 28, TextAnchor.UpperRight);
        weightBox2.transform.anchoredPosition = new Vector2(-5, -64);
        TextBox hpBox1 = new TextBox(transform, "HP", 28, TextAnchor.UpperLeft);
        hpBox1.transform.anchoredPosition = new Vector2(5, -94);
        TextBox hpBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.hitpoints), 28, TextAnchor.UpperRight);
        hpBox2.transform.anchoredPosition = new Vector2(-5, -94);
        TextBox armorBox1 = new TextBox(transform, "Armor", 28, TextAnchor.UpperLeft);
        armorBox1.transform.anchoredPosition = new Vector2(5, -124);
        TextBox armorBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.equipment.Armor().ToString("00%")), 28, TextAnchor.UpperRight);
        armorBox2.transform.anchoredPosition = new Vector2(-5, -124);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("inventory"))
        {
            switch (windowstance)
            {
            case WindowStance.non: SwitchWindowStance(WindowStance.inventory); break;
            case WindowStance.inventory: SwitchWindowStance(WindowStance.non); break;
            case WindowStance.shop: SwitchWindowStance(WindowStance.non); break;
            }
        }
        if (Input.GetButtonDown("Cancel"))
        {
            SwitchWindowStance(WindowStance.non);
        }
    }

    public static void SwitchWindowStance(WindowStance stance)
    {
        if (stance == windowstance) return;
        if(windowstance == WindowStance.non)
        {
            Time.timeScale = 0;
            switch (stance)
            {
            case WindowStance.inventory: UI_inventory.go.SetActive(true); break;
            case WindowStance.shop: UI_shop.go.SetActive(true); break;
            default: throw new System.Exception("Invalid stance");
            }
        }else 
        {
            UI_inventory.go.SetActive(false);
            UI_shop.go.SetActive(false);
            switch (stance)
            {
            case WindowStance.non: Time.timeScale = 1; break;
            case WindowStance.inventory: UI_inventory.go.SetActive(true); break;
            case WindowStance.shop: UI_shop.go.SetActive(true); break;
            default: throw new System.Exception("Invalid stance");
            }
        }
        windowstance = stance;
    }


}
