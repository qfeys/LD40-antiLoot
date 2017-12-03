using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using QuintensUITools;

static class UI_shop
{
    static public GameObject go;
    static public List<Loot> inventory = new List<Loot>();
    static List<TextBox> equipmentTitles;
    static TextBox equipmentHighlight;
    static InfoTable shopInventoryTable;
    static InfoTable myInventoryTable;

    static public void Create(GameObject canvas)
    {
        go = new GameObject("Shop_Screen", typeof(RectTransform));
        go.transform.SetParent(canvas.transform);
        go.AddComponent<Dragable>();
        RectTransform tr = (RectTransform)go.transform;
        tr.sizeDelta = new Vector2(920, 600);
        tr.anchorMin = new Vector2(.5f, .5f);
        tr.anchorMax = new Vector2(.5f, .5f);
        tr.pivot = new Vector2(.5f, .5f);
        tr.anchoredPosition = new Vector2(0, 0);
        Image im = go.AddComponent<Image>();
        im.sprite = QuintensUITools.Graphics.GetSprite("Inventory_window");
        im.type = Image.Type.Sliced;

        /// Table with the shops inventory
        {
            shopInventoryTable = InfoTable.Create(go.transform, () => inventory,
                (Loot l) => new List<TextRef>() { l.ToString(), GetSpecificDetails(l), l.weight, l.value, l.slot.ToString() },
                460, new List<TextRef>() { "Inventory", "Specifics", TextRef.Create("w", "Weight", false), TextRef.Create("v", "value", false), "s" }, 24, "Me");
            shopInventoryTable.transform.anchorMin = new Vector2(0, 1);
            shopInventoryTable.transform.anchorMax = new Vector2(0, 1);
            shopInventoryTable.transform.pivot = new Vector2(0, 1);
            shopInventoryTable.transform.anchoredPosition = new Vector2(10, -50);
            shopInventoryTable.SetColumnWidths(new List<float>() { 160, 80, 50, 50, 160 });
        }
        /// Table with my inventory
        {
            myInventoryTable = InfoTable.Create(go.transform, () => Player.instance.inventory,
                (Loot l) => new List<TextRef>() { l.ToString(), GetSpecificDetails(l), l.weight, l.value, l.slot.ToString() },
                460, new List<TextRef>() { "Inventory", "Specifics", TextRef.Create("w", "Weight", false), TextRef.Create("v", "value", false), "s" }, 24, "Me");
            myInventoryTable.transform.anchorMin = new Vector2(1, 1);
            myInventoryTable.transform.anchorMax = new Vector2(1, 1);
            myInventoryTable.transform.pivot = new Vector2(1, 1);
            myInventoryTable.transform.anchoredPosition = new Vector2(-10, -50);
            myInventoryTable.SetColumnWidths(new List<float>() { 160, 80, 50, 50, 160 });
        }

        /// Bottom buttons
        {
            TextBox equipButton = new TextBox(go.transform,
              TextRef.Create("Buy", "Make sure your product is selected.", false).AddLink(() => BuySelected()),
              24);
            equipButton.transform.anchorMin = new Vector2(0, 0);
            equipButton.transform.anchorMax = new Vector2(0, 0);
            equipButton.transform.pivot = new Vector2(0, 0);
            equipButton.transform.anchoredPosition = new Vector2(40, 40);
            TextBox dropAllButton = new TextBox(go.transform,
                TextRef.Create("Sell All", "Sell everything in your inventory.\nBeware! You can't buy it back.", false).AddLink(() => DiscardAll()),
                24, TextAnchor.MiddleRight);
            dropAllButton.transform.anchorMin = new Vector2(1, 0);
            dropAllButton.transform.anchorMax = new Vector2(1, 0);
            dropAllButton.transform.pivot = new Vector2(1, 0);
            dropAllButton.transform.anchoredPosition = new Vector2(-150, 40);
            TextBox dropButton = new TextBox(go.transform,
                TextRef.Create("Sell", "-Select an item.\n-Press this button.", false).AddLink(() => DiscardSelected()),
                24, TextAnchor.MiddleRight);
            dropButton.transform.anchorMin = new Vector2(1, 0);
            dropButton.transform.anchorMax = new Vector2(1, 0);
            dropButton.transform.pivot = new Vector2(1, 0);
            dropButton.transform.anchoredPosition = new Vector2(-40, 40);
        }
        go.SetActive(false);
    }

    private static TextRef GetSpecificDetails(Loot l)
    {
        if (l.GetType() == typeof(Loot.Melee))
        {
            Loot.Melee m = (Loot.Melee)l;
            return TextRef.Create("d: " + TextRef.ToSI(m.damage) + ", r:" + TextRef.ToSI(m.range),
                "Damage: " + TextRef.ToSI(m.damage) + "\nRange: " + TextRef.ToSI(m.range), false);
        }
        else if (l.GetType() == typeof(Loot.Ranged))
        {
            Loot.Ranged r = (Loot.Ranged)l;
            return TextRef.Create("d: " + TextRef.ToSI(r.damage) + ", r:" + TextRef.ToSI(r.range),
                "Damage: " + TextRef.ToSI(r.damage) + "\nRange: " + TextRef.ToSI(r.range), false);
        }
        else if (l.GetType() == typeof(Loot.Shield))
        {
            Loot.Shield s = (Loot.Shield)l;
            return TextRef.Create("p: " + TextRef.ToSI(s.blockChancePassive) + ", a:" + TextRef.ToSI(s.blockChanceActive), 
                "Passive block chance: " + TextRef.ToSI(s.blockChancePassive) + "\nActive block chance: " + TextRef.ToSI(s.blockChanceActive), false);
        }
        else if (l.GetType() == typeof(Loot.Armor))
        {
            Loot.Armor a = (Loot.Armor)l;
            return TextRef.Create("b: " + TextRef.ToSI(a.blockChance), "Block chance: " + TextRef.ToSI(a.blockChance), false);
        }


        else
            throw new Exception("This switsh statement did not acount for: " + l.GetType().ToString());

    }

    private static void BuySelected()
    {
        Loot lt = shopInventoryTable.RetrieveHighlight<Loot>();
        if (lt == null) return;
        if (Player.instance.coins < (int)lt.value) return;
        Player.instance.coins -= (int)lt.value;
        Player.instance.inventory.Add(lt);
    }

    private static void DiscardAll()
    {
        Player.instance.inventory.ForEach(l => Player.instance.coins += (int)l.value);
        Player.instance.inventory.RemoveAll(l => true);
    }

    private static void DiscardSelected()
    {
        Loot lt = myInventoryTable.RetrieveHighlight<Loot>();
        if (lt == null) return;
        Player.instance.coins += (int)lt.value;
        Player.instance.inventory.Remove(lt);
    }
}
