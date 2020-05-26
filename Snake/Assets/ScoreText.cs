using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreText : MonoBehaviour
{
    private TextMeshPro textMash;
    public static int Score = 0;

    private void Awake()
    {
        GameHandler.Instance.AddFoodEatListener(AddScore);
    }

    void Start()
    {
        Score = 0;
        textMash = GetComponent<TextMeshPro>();
    }

    private void AddScore()
    {
        Score += 1000;
        textMash.SetText("Score: " + Score);
    }

}
