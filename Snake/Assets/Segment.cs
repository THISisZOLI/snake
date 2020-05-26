using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Segment : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private Snake head;
    private float delay;
    private Queue<TimePositionData> positions = new Queue<TimePositionData>();

    public void Initialize(Snake head, float delay, Queue<TimePositionData> positions)
    {
        this.head = head;
        this.delay = delay;
        if (positions != null)
        {
            this.positions = new Queue<TimePositionData>(positions);
        }
        head.OnMovement += OnMovement;
    }

    void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (GameHandler.Instance.IsOver)
        {
            return;
        }
        while (positions.Count != 0 && positions.Peek().time <= Time.fixedTime - delay)
        {
            rigidBody.MovePosition(positions.Dequeue().position);
        }
    }

    private void OnMovement(TimePositionData data)
    {
        positions.Enqueue(data);
    }

    public Vector2 GetPosition()
    {
        return rigidBody.position;
    }

    public Queue<TimePositionData> GetPositions()
    {
        return positions;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Equals("Snake") && Vector2.Distance(collision.GetComponent<Rigidbody2D>().position, GetPosition()) > 0.2f)
        {
            GameHandler.Instance.GameOver();
        }
    }
}
