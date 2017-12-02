using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using QuintensUITools;

static class UI_inventory
{
    static public GameObject go;

    static public void Create(GameObject canvas)
    {
        go = new GameObject("Inventory", typeof(RectTransform));
        go.transform.SetParent(canvas.transform);
        go.AddComponent<Dragable>();
        RectTransform tr = (RectTransform)go.transform;
        tr.sizeDelta = new Vector2(600, 600);
        tr.anchorMin = new Vector2(.5f, .5f);
        tr.anchorMax = new Vector2(.5f, .5f);
        tr.pivot = new Vector2(.5f, .5f);
        tr.anchoredPosition = new Vector2(0, 0);
        Image im = go.AddComponent<Image>();
        im.sprite = QuintensUITools.Graphics.GetSprite("Inventory_window");
        im.type = Image.Type.Sliced;

        InfoTable inv = InfoTable.Create(go.transform, () => Player.instance.inventory,
            (Loot l) => new List<TextRef>() { l.ToString(), GetSpecificDetails(l), l.weight, l.value, l.slot.ToString() },
            360, new List<TextRef>() { "Inventory", "Specifics", "w", "v", "s" }, 24);
        inv.transform.anchorMin = new Vector2(1, 1);
        inv.transform.anchorMax = new Vector2(1, 1);
        inv.transform.pivot = new Vector2(1, 1);
        inv.transform.anchoredPosition = new Vector2(-10, -50);
        inv.SetColumnWidths(new List<float>() { 200, 40, 40, 40, 80 });

        go.SetActive(false);
    }

    private static TextRef GetSpecificDetails(Loot l)
    {
        if (l.GetType() == typeof(Loot.Melee))
        {
            Loot.Melee m = (Loot.Melee)l;
            return TextRef.Create("d: " + TextRef.ToSI(m.damage, "0.##") + ", r:" + TextRef.ToSI(m.range, "0.##"),
                "Damage: " + TextRef.ToSI(m.damage, "0.##") + "\nRange: " + TextRef.ToSI(m.range, "0.##"), false);
        }
        else if (l.GetType() == typeof(Loot.Ranged))
        {
            Loot.Ranged r = (Loot.Ranged)l;
            return TextRef.Create("d: " + TextRef.ToSI(r.damage, "0.##") + ", r:" + TextRef.ToSI(r.range, "0.##"),
                "Damage: " + TextRef.ToSI(r.damage, "0.##") + "\nRange: " + TextRef.ToSI(r.range, "0.##"), false);
        }
        else if (l.GetType() == typeof(Loot.Shield))
        {
            Loot.Shield s = (Loot.Shield)l;
            return TextRef.Create("p: " + s.blockChancePassive + ", a:" + s.blockChanceActive, 
                "Passive block chance: " + s.blockChancePassive + "\nActive block chance: " + s.blockChanceActive, false);
        }
        else if (l.GetType() == typeof(Loot.Armor))
        {
            Loot.Armor a = (Loot.Armor)l;
            return TextRef.Create("b: " + a.blockChance, "Block chance: " + a.blockChance, false);
        }


        else
            throw new Exception("This switsh statement did not acount for: " + l.GetType().ToString());
        
    }
}
