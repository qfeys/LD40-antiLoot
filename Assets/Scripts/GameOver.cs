using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour {

    public Text distanceText;
    public float distance { get { return _distance; } set { _distance = value; distanceText.text = QuintensUITools.TextRef.ToSI(value); } }
    public float _distance;

    public void ToMainMenu()
    {
        Time.timeScale = 1;
        UI_Stats.windowstance = UI_Stats.WindowStance.non;
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

}
