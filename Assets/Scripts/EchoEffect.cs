using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EchoEffect : MonoBehaviour
{
    private float timeBtwSpawns;
    public float startTimeBtwSpawns;

    public GameObject[] echos;
    Ball ball;

    void Awake()
    {
        ball = GetComponent<Ball>();
    }

    void Update()
    {
        if (ball.Velocity != Vector2.zero)
        {
            if (timeBtwSpawns <= 0)
            {
                // To Refactor
                GameObject instance = Instantiate(echos[0], transform.position, Quaternion.identity);
                Destroy(instance, 0.2f);

                GameObject instance1 = Instantiate(echos[1], transform.position, Quaternion.identity);
                Destroy(instance1, 0.5f);

                GameObject instance2 = Instantiate(echos[2], transform.position, Quaternion.identity);
                Destroy(instance2, 0.9f);
                timeBtwSpawns = startTimeBtwSpawns;
            }
            else
            {
                timeBtwSpawns -= Time.deltaTime;
            }
        }
    }
}