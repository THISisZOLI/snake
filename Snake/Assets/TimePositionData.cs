using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimePositionData
{
    public float time;
    public Vector2 position;
    public TimePositionData(float t, Vector2 pos)
    {
        time = t;
        position = pos;
    }
}
