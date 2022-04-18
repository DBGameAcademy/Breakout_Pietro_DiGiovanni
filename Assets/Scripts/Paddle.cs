using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float MoveSpeed;
    public Vector2 InitialSize;
    public Vector2 InitialPosition;

    void Start()
    {
        InitialSize = GetComponent<SpriteRenderer>().size;
        InitialPosition = new Vector2(0, -4.5f);
    }

    public void Move(Vector2 _direction)
    {
        transform.Translate(_direction * MoveSpeed * Time.deltaTime);
    }
}
