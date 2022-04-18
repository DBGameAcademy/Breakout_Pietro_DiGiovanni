using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyBall : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        rb.velocity = new Vector2(transform.position.x,rb.velocity.y * speed * Time.deltaTime);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<Block>())
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);
        }
        else if (collision.gameObject.GetComponent<Paddle>())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Abs(rb.velocity.y));
        }
    }
}
