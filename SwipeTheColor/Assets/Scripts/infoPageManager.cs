using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
public class infoPageManager : MonoBehaviour {
    public Text gameTimeText;
    public Text swipeTimeText;
    public Text highScoreText;

    string path = "Assets/info.txt";
    string gamePath = "Assets/gameInfo.txt";

    int highScore;
    int gameTime;
    int swipeTime;
    // Use this for initialization
    void Start () {
        /* StreamReader reader = new StreamReader(gamePath);

        gameTime = int.Parse(reader.ReadLine(), System.Globalization.NumberStyles.Integer);
        swipeTime = int.Parse(reader.ReadLine(), System.Globalization.NumberStyles.Integer);
        highScore = int.Parse(reader.ReadLine(), System.Globalization.NumberStyles.Integer);
        reader.Close();*/
        highScore = PlayerPrefs.GetInt("highscore", highScore);
        gameTime = PlayerPrefs.GetInt("gameTime", (int)gameTime);
        swipeTime = PlayerPrefs.GetInt("swipeTime", swipeTime);
        gameTimeText.text = "-PLAYED TIME-\n\n" + gameTime+"\nSECONDS";
        swipeTimeText.text = "-SWIPE COUNTER-\n\n" + swipeTime+"\nTIMES";
        highScoreText.text = "-HIGH SCORE-\n\n" + highScore;
    }
	
	
}
