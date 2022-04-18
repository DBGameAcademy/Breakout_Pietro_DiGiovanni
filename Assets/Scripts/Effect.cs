using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour
{
    public float Speed = 5f;
    public float CountDown = 5f;
    public bool isActive = false;

    Paddle Paddle;

    private void Awake()
    {
        Paddle = FindObjectOfType<Paddle>();
    }

    void Update()
    {
        transform.Translate(Vector2.down * Speed * Time.deltaTime);

        if (isActive)
        {
            CountDown -= Time.deltaTime;

            if (CountDown <= 0)
            {
                isActive = false;
                CountDown = 5f;
            }
        }
        else
        {
            Paddle.GetComponent<SpriteRenderer>().size = Paddle.InitialSize;
            Paddle.GetComponent<BoxCollider2D>().size = Paddle.InitialSize;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Paddle>())
        {
            if (gameObject.CompareTag("PowerUp"))
            {
                isActive = true;
                IncreasePaddleSize();
            }
            if (gameObject.CompareTag("Malus"))
            {
                isActive = true;
                DecreasePaddleSize();
            }
            Destroy(gameObject);
        }
    }

    void IncreasePaddleSize()
    {
        Vector2 largeSize = new Vector2(1.5f, 0.31f);
        Paddle.GetComponent<SpriteRenderer>().size = largeSize;
        Paddle.GetComponent<BoxCollider2D>().size = largeSize;
    }

    void DecreasePaddleSize()
    {
        Vector2 smallSize = new Vector2(0.5f, 0.31f);
        Paddle.GetComponent<SpriteRenderer>().size = smallSize;
        Paddle.GetComponent<BoxCollider2D>().size = smallSize;

    }
}
