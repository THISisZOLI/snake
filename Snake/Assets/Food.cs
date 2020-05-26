using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public delegate void FoodEatenHandler();
    public event FoodEatenHandler OnFoodEaten;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Snake"))
        {
            Destroy(gameObject);
            OnFoodEaten?.Invoke();
        }
    }
}
