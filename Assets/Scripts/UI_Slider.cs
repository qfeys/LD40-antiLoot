using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UI_Slider : MonoBehaviour {

    Slider slider;

	// Use this for initialization
	void Start () {
        slider = GetComponent<Slider>();
	}
	
	// Update is called once per frame
	void Update () {
        float totalWeight = Player.instance.inventory.Sum(l => l.weight);
        float carryingCap = Player.carryingCap;
        float ratio = totalWeight / carryingCap;

        float encumburance = 1.1f - 1.1f / (1 + 10 * Mathf.Exp(-Mathf.Pow(ratio, 2)));
        Player.encumburance = encumburance;

        Color target = ratio < 1 ? Color.Lerp(Color.HSVToRGB(0.15f, 0.58f, 0.74f), Color.HSVToRGB(0.04f, 0.69f, 0.74f), ratio) :
            Color.Lerp(Color.HSVToRGB(0.04f, 0.69f, 0.74f), Color.HSVToRGB(0.00f, 1, 0.30f), ratio - 1);
        slider.fillRect.transform.GetComponent<Image>().color = target;
        slider.value = ratio;
	}
}
