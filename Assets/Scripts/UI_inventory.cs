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
    static List<TextBox> equipmentTitles;
    static TextBox equipmentHighlight;
    static InfoTable inventoryTable;

    static public void Create(GameObject canvas)
    {
        go = new GameObject("Inventory", typeof(RectTransform));
        go.transform.SetParent(canvas.transform);
        go.AddComponent<Dragable>();
        RectTransform tr = (RectTransform)go.transform;
        tr.sizeDelta = new Vector2(700, 600);
        tr.anchorMin = new Vector2(.5f, .5f);
        tr.anchorMax = new Vector2(.5f, .5f);
        tr.pivot = new Vector2(.5f, .5f);
        tr.anchoredPosition = new Vector2(0, 0);
        Image im = go.AddComponent<Image>();
        im.sprite = QuintensUITools.Graphics.GetSprite("Inventory_window");
        im.type = Image.Type.Sliced;
        /// Equipment
        {
            TextBox head = new TextBox(go.transform, TextRef.Create("Head", "Hats and stuff", false), 24, TextAnchor.UpperLeft);
            head.transform.anchoredPosition = new Vector2(10, -80);
            TextBox head2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.head.GetItemName(), () => Player.instance.equipment.head.GetItemStats()), 24, TextAnchor.UpperLeft);
            head2.transform.anchoredPosition = new Vector2(60, -100);
            MakeEquButton(head);
            TextBox chest = new TextBox(go.transform, TextRef.Create("Chest", "Upper body protection", false), 24, TextAnchor.UpperLeft);
            chest.transform.anchoredPosition = new Vector2(10, -130);
            TextBox chest2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.chest.GetItemName(), () => Player.instance.equipment.chest.GetItemStats()), 24, TextAnchor.UpperLeft);
            chest2.transform.anchoredPosition = new Vector2(60, -150);
            MakeEquButton(chest);
            TextBox rhand = new TextBox(go.transform, TextRef.Create("Right Hand", "The weapon hand", false), 24, TextAnchor.UpperLeft);
            rhand.transform.anchoredPosition = new Vector2(10, -180);
            TextBox rhand2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.rHand.GetItemName(), () => Player.instance.equipment.rHand.GetItemStats()), 24, TextAnchor.UpperLeft);
            rhand2.transform.anchoredPosition = new Vector2(60, -200);
            MakeEquButton(rhand);
            TextBox lhand = new TextBox(go.transform, TextRef.Create("Left Hand", "The shield hand", false), 24, TextAnchor.UpperLeft);
            lhand.transform.anchoredPosition = new Vector2(10, -230);
            TextBox lhand2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.lHand.GetItemName(), () => Player.instance.equipment.lHand.GetItemStats()), 24, TextAnchor.UpperLeft);
            lhand2.transform.anchoredPosition = new Vector2(60, -250);
            MakeEquButton(lhand);
            TextBox rarm = new TextBox(go.transform, TextRef.Create("Right arm", "Arm protection", false), 24, TextAnchor.UpperLeft);
            rarm.transform.anchoredPosition = new Vector2(10, -280);
            TextBox rarm2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.rArm.GetItemName(), () => Player.instance.equipment.rArm.GetItemStats()), 24, TextAnchor.UpperLeft);
            rarm2.transform.anchoredPosition = new Vector2(60, -300);
            MakeEquButton(rarm);
            TextBox larm = new TextBox(go.transform, TextRef.Create("Left arm", "Arm protection", false), 24, TextAnchor.UpperLeft);
            larm.transform.anchoredPosition = new Vector2(10, -330);
            TextBox larm2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.lArm.GetItemName(), () => Player.instance.equipment.lArm.GetItemStats()), 24, TextAnchor.UpperLeft);
            larm2.transform.anchoredPosition = new Vector2(60, -350);
            MakeEquButton(larm);
            TextBox rleg = new TextBox(go.transform, TextRef.Create("Right leg", "Leg protection", false), 24, TextAnchor.UpperLeft);
            rleg.transform.anchoredPosition = new Vector2(10, -380);
            TextBox rleg2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.rLeg.GetItemName(), () => Player.instance.equipment.rLeg.GetItemStats()), 24, TextAnchor.UpperLeft);
            rleg2.transform.anchoredPosition = new Vector2(60, -400);
            MakeEquButton(rleg);
            TextBox lleg = new TextBox(go.transform, TextRef.Create("Left leg", "Leg protection", false), 24, TextAnchor.UpperLeft);
            lleg.transform.anchoredPosition = new Vector2(10, -430);
            TextBox lleg2 = new TextBox(go.transform, TextRef.Create(() => Player.instance.equipment.lLeg.GetItemName(), () => Player.instance.equipment.lLeg.GetItemStats()), 24, TextAnchor.UpperLeft);
            lleg2.transform.anchoredPosition = new Vector2(60, -450);
            MakeEquButton(lleg);
        }

        /// Table with the inventory
        {
            inventoryTable = InfoTable.Create(go.transform, () => Player.instance.inventory,
                (Loot l) => new List<TextRef>() { l.ToString(), GetSpecificDetails(l), l.weight, l.value, l.slot.ToString() },
                460, new List<TextRef>() { "Inventory", "Specifics", TextRef.Create("w", "Weight", false), TextRef.Create("v", "value", false), "s" }, 24);
            inventoryTable.transform.anchorMin = new Vector2(1, 1);
            inventoryTable.transform.anchorMax = new Vector2(1, 1);
            inventoryTable.transform.pivot = new Vector2(1, 1);
            inventoryTable.transform.anchoredPosition = new Vector2(-10, -50);
            inventoryTable.SetColumnWidths(new List<float>() { 160, 80, 50, 50, 160 });
        }

        /// Bottom buttons
        {
            TextBox equipButton = new TextBox(go.transform,
              TextRef.Create("Equip", "-Select a weapon.\n-Select a slot.\n-Press this button.", false).AddLink(() => EquipSelected()),
              24);
            equipButton.transform.anchorMin = new Vector2(0, 0);
            equipButton.transform.anchorMax = new Vector2(0, 0);
            equipButton.transform.pivot = new Vector2(0, 0);
            equipButton.transform.anchoredPosition = new Vector2(40, 40);
            TextBox dropAllButton = new TextBox(go.transform,
                TextRef.Create("Discard All", "Throw away everything in your inventory.\nBeware! All will be lost.", false).AddLink(() => DiscardAll()),
                24, TextAnchor.MiddleRight);
            dropAllButton.transform.anchorMin = new Vector2(1, 0);
            dropAllButton.transform.anchorMax = new Vector2(1, 0);
            dropAllButton.transform.pivot = new Vector2(1, 0);
            dropAllButton.transform.anchoredPosition = new Vector2(-150, 40);
            TextBox dropButton = new TextBox(go.transform,
                TextRef.Create("Discard", "-Select an item.\n-Press this button.", false).AddLink(() => DiscardSelected()),
                24, TextAnchor.MiddleRight);
            dropButton.transform.anchorMin = new Vector2(1, 0);
            dropButton.transform.anchorMax = new Vector2(1, 0);
            dropButton.transform.pivot = new Vector2(1, 0);
            dropButton.transform.anchoredPosition = new Vector2(-40, 40);
        }
        go.SetActive(false);
    }

    private static void MakeEquButton(TextBox equ)
    {
        if (equipmentTitles == null) equipmentTitles = new List<TextBox>();
        equipmentTitles.Add(equ);
        GameObject go = new GameObject("EquipmentTitle", typeof(RectTransform));
        go.transform.parent = equ.transform.parent;
        RectTransform rt = go.transform as RectTransform;
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.anchoredPosition = equ.transform.anchoredPosition;
        rt.sizeDelta = new Vector2(100, 50);
        go.AddComponent<Image>().color = new Color(0, 0, 0, 0);
        Button but = go.AddComponent<Button>();
        but.transition = Selectable.Transition.None;
        but.onClick.AddListener(() => MakeHighlight(equ));
    }

    private static void MakeHighlight(TextBox equ)
    {
        equipmentTitles.ForEach(tb => tb.SetColor(QuintensUITools.Graphics.Color_.text));
        equ.SetColor(QuintensUITools.Graphics.Color_.activeText);
        equipmentHighlight = equ;
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
            return TextRef.Create("p: " + s.blockChancePassive.ToString("#0%") + ", a:" + s.blockChanceActive.ToString("#0%"),
                "Passive block chance: " + s.blockChancePassive.ToString("#0%") + "\nActive block chance: " + s.blockChanceActive.ToString("#0%"), false);
        }
        else if (l.GetType() == typeof(Loot.Armor))
        {
            Loot.Armor a = (Loot.Armor)l;
            return TextRef.Create("b: " + a.blockChance.ToString("#0%"), "Block chance: " + a.blockChance.ToString("#0%"), false);
        }


        else
            throw new Exception("This switsh statement did not acount for: " + l.GetType().ToString());

    }

    private static void EquipSelected()
    {
        Loot lt = inventoryTable.RetrieveHighlight<Loot>();
        Player.ItemSlot itms = null;
        switch (equipmentHighlight.Text)
        {
        case "Head":
            itms = Player.instance.equipment.head; break;
        case "Chest":
            itms = Player.instance.equipment.chest; break;
        case "Right Hand":
            itms = Player.instance.equipment.rHand; break;
        case "Left Hand":
            itms = Player.instance.equipment.lHand; break;
        case "Right arm":
            itms = Player.instance.equipment.rArm; break;
        case "Left arm":
            itms = Player.instance.equipment.lArm; break;
        case "Right leg":
            itms = Player.instance.equipment.rLeg; break;
        case "Left leg":
            itms = Player.instance.equipment.lLeg; break;
        default:
            throw new Exception("Stupid shit in equipment");
        }
        if (lt.slot == itms.slot)
        {
            if (itms.item != null)
                Player.instance.inventory.Add(itms.item);
            itms.item = lt;
            inventoryTable.Redraw();
            Player.instance.inventory.Remove(lt);
        }
        else Debug.Log("Not a valid slot");
    }

    private static void DiscardAll()
    {
        Player.instance.inventory.RemoveAll(l => true);
    }

    private static void DiscardSelected()
    {
        Loot lt = inventoryTable.RetrieveHighlight<Loot>();
        Player.instance.inventory.Remove(lt);
    }
}
