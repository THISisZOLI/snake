using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }

    public Food FoodPrefab;
    public EndMenu endMenu;
    private List<Food.FoodEatenHandler> delegates = new List<Food.FoodEatenHandler>();

    public bool IsOver = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        delegates.Add(SpawnFood);
        SpawnFood();
        SpawnFood();
    }

    private void SpawnFood()
    {
        bool spawned = false;
        int maxTries = 100;
        while (!spawned && maxTries > 0)
        {
            Vector3 pos = new Vector3(Random.Range(-16.5f, 16.5f), Random.Range(-8.5f, 8.5f), 0);
            if (Physics2D.OverlapCircleAll(pos, 1).Length == 0)
            {
                Food foodObject = Instantiate(FoodPrefab, pos, Quaternion.identity);
                foreach(Food.FoodEatenHandler handler in delegates)
                {
                    foodObject.OnFoodEaten += handler;
                }
                spawned = true;
            }
            maxTries--;
        }
    }

    public void AddFoodEatListener(Food.FoodEatenHandler handler)
    {
        delegates.Add(handler);
    }

    public void GameOver()
    {
        endMenu.GameOver();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("menuScene");
        }
    }
}
