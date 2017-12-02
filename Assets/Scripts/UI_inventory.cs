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
        go = new GameObject("Inspector", typeof(RectTransform));
        go.transform.SetParent(canvas.transform);
        RectTransform tr = (RectTransform)go.transform;
        tr.sizeDelta = new Vector2(400, 400);
        tr.anchorMin = new Vector2(.5f, .5f);
        tr.anchorMax = new Vector2(.5f, .5f);
        tr.pivot = new Vector2(.5f, .5f);
        tr.anchoredPosition = new Vector2(0, 0);
        Image im = go.AddComponent<Image>();
        im.sprite = QuintensUITools.Graphics.GetSprite("Inventory_window");
        im.type = Image.Type.Filled;

        InfoTable inv = InfoTable.Create(go.transform, () => Player.instance.inventory, (Loot l) => new List<TextRef>() { TextRef.Create(() => l) },
            200, new List<TextRef>() { "Inventory" }, 24);
        inv.transform.anchorMin = new Vector2(1, 1);
        inv.transform.anchorMax = new Vector2(1, 1);
        inv.transform.pivot = new Vector2(1, 1);
        inv.transform.anchoredPosition = new Vector2(-10, -50);

        go.SetActive(false);
    }

}
