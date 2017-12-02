using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using QuintensUITools;

public class UI_Stats : MonoBehaviour {

    public Font DefaultFont;

	// Use this for initialization
	void Start ()
    {
        QuintensUITools.Graphics.SetDefaultFont(DefaultFont);
        QuintensUITools.Graphics.SetPath(@"Assets\");
        QuintensUITools.Graphics.LoadGraphics();
        UI_inventory.Create(transform.parent.gameObject);

        CreateMe();

    }

    private void CreateMe()
    {
        TextBox xpBox1 = new TextBox(transform, TextRef.Create( "Xp", "Experiance", false), 36);
        xpBox1.transform.anchoredPosition = new Vector2(5, 38);
        TextBox xpBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.xp), 36, TextAnchor.MiddleRight);
        xpBox2.transform.anchoredPosition = new Vector2(-5, 38);
        TextBox coinBox1 = new TextBox(transform, "Coins", 36);
        coinBox1.transform.anchoredPosition = new Vector2(5, 0);
        TextBox coinBox2 = new TextBox(transform, TextRef.Create(() => Player.instance.coins), 36, TextAnchor.MiddleRight);
        coinBox2.transform.anchoredPosition = new Vector2(-5, 0);
        TextBox weightBox1 = new TextBox(transform, "Weight", 36);
        weightBox1.transform.anchoredPosition = new Vector2(5, -38);
        TextBox weightBox2 = new TextBox(transform, "###", 36, TextAnchor.MiddleRight);
        weightBox2.transform.anchoredPosition = new Vector2(-5, -38);
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetButtonDown("inventory"))
        {
            UI_inventory.go.SetActive(!UI_inventory.go.activeSelf);
        }
	}
}
