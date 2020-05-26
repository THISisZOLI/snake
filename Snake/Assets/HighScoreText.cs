using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HighScoreText : MonoBehaviour
{
    void Start()
    {
        GetComponent<TextMeshPro>().SetText("High Score: " + PlayerPrefs.GetString("HighScoreName", "ZOZ") + " - " + PlayerPrefs.GetInt("HighScore", 2000).ToString());
    }
}
