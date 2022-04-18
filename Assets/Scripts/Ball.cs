using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    CircleCollider2D circleCollider2D;

    public Vector2 Velocity = new Vector2(4f, 4f);

    GameObject lastObjectHit;

    public AudioClip OnWallHitAudio;
    public AudioClip OnPaddleHitAudio;

    void Awake()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
    }

    void FixedUpdate()
    {
        transform.Translate(Velocity * Time.deltaTime);

        RaycastHit2D[] ballHits = Physics2D.CircleCastAll(transform.position, circleCollider2D.radius, Velocity, (Velocity * Time.deltaTime).magnitude);

        foreach (RaycastHit2D hit in ballHits)
        {
            if (hit.collider != circleCollider2D && hit.transform.gameObject != lastObjectHit)
            {
                lastObjectHit = hit.transform.gameObject;
                Velocity = Vector2.Reflect(Velocity, hit.normal);

                if (hit.transform.GetComponent<Paddle>())
                {
                    Velocity.y = Mathf.Abs(Velocity.y);
                    AudioController.Instance.PlayClip(OnPaddleHitAudio);
                }

                if (hit.transform.GetComponent<Block>())
                {
                    hit.transform.GetComponent<Block>().OnHit();
                }
                AudioController.Instance.PlayClip(OnWallHitAudio);
            }
        }

        if(transform.position.y < -Camera.main.orthographicSize)
        {
            GameController.Instance.LostBall();
        }
    }
}
