using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EndMenu : MonoBehaviour
{
    public GameObject EndMenuUI;
    public GameObject GameOverUI;

    public Button Letter1;
    public Button Letter2;
    public Button Letter3;

    private List<Button> buttons;
    private bool newHighScore = false;

    public float InputDelay;

    private float prevInputTime = 0;
    private readonly string alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

    void Start()
    {
        buttons = new List<Button>() { Letter1, Letter2, Letter3 };
    }

    private void ResetButtons()
    {
        foreach (Button b in buttons)
        {
            b.GetComponentInChildren<TextMeshProUGUI>().SetText("A");
        }
    }

    private Button GetSelectedButton()
    {
        Button[] buttons = { Letter1, Letter2, Letter3 };
        GameObject current = EventSystem.current.currentSelectedGameObject;
        foreach(Button b in buttons)
        {
            if (b.gameObject == current)
            {
                return b;
            }
        }
        return null;
    }

    private void UpdateButton(Button b, int step)
    {
        TextMeshProUGUI textMesh = b.GetComponentInChildren<TextMeshProUGUI>();
        int len = alphabet.Length;
        int curr = alphabet.IndexOf(textMesh.text);
        textMesh.text = alphabet[(len + curr + step) % len].ToString();
    }

    void Update()
    {
        if (!GameHandler.Instance.IsOver)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.JoystickButton0))
        {
            Close();
        }
        if (!newHighScore)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UpdateButton(GetSelectedButton(), 1);
            prevInputTime = Time.time;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UpdateButton(GetSelectedButton(), -1);
            prevInputTime = Time.time;
        }
        else if (Time.time - InputDelay < prevInputTime)
        {
            return;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            UpdateButton(GetSelectedButton(), 1);
            prevInputTime = Time.time;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            UpdateButton(GetSelectedButton(), -1);
            prevInputTime = Time.time;
        }
    }

    private void Close()
    {
        if (newHighScore)
        {
            PlayerPrefs.SetInt("HighScore", ScoreText.Score);
            string name = "";
            foreach (Button b in buttons)
            {
                name += b.GetComponentInChildren<TextMeshProUGUI>().text;
            }
            PlayerPrefs.SetString("HighScoreName", name);
        }
        SceneManager.LoadScene("menuScene");
    }

    public void GameOver()
    {
        if (PlayerPrefs.GetInt("HighScore", 0) < ScoreText.Score)
        {
            newHighScore = true;
            ResetButtons();
            EndMenuUI.SetActive(true);
            Letter1.Select();
        }
        else
        {
            GameOverUI.SetActive(true);
        }
        GameHandler.Instance.IsOver = true;
    }
}
