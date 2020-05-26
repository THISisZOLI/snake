using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    public delegate void MovementHandler(TimePositionData data);
    public event MovementHandler OnMovement;

    private Rigidbody2D rigidBody;
    Vector2 direction = new Vector2(0, 0);

    public float Velocity;
    public Segment SegmentPrefab;
    public float delay;

    private List<TimePositionData> positions = new List<TimePositionData>();
    private float currentDelay;
    private Segment lastSegment;

    void Start()
    {
        GameHandler.Instance.AddFoodEatListener(SpawnSegment);
        rigidBody = GetComponent<Rigidbody2D>();
        currentDelay = delay;
    }

    void Update()
    {
        Vector2 newDirection = GetDirection();
        if (!newDirection.Equals(direction) && newDirection + direction != Vector2.zero)
        {
            direction = newDirection;
        }
    }

    void FixedUpdate()
    {
        if (GameHandler.Instance.IsOver)
        {
            return;
        }
        rigidBody.MovePosition(rigidBody.position + direction * Velocity / 100);
        OnMovement?.Invoke(new TimePositionData(Time.fixedTime, rigidBody.position));
    }

    private Vector2 GetDirection()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            return Vector2.right;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            return Vector2.up;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            return Vector2.left;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            return Vector2.down;
        }
        return direction;
    }

    private void SpawnSegment()
    {
        Segment segmentObject = Instantiate(SegmentPrefab, lastSegment == null ? rigidBody.position : lastSegment.GetPosition(), Quaternion.identity);
        segmentObject.Initialize(this, currentDelay, lastSegment?.GetPositions());
        lastSegment = segmentObject;
        currentDelay += delay;
    }
}