using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    int hits = 1;
    public int scoreValue = 100;

    SpriteRenderer _blockSprite;

    public AudioClip OnBreakAudio;

    void Awake()
    {
        _blockSprite = GetComponent<SpriteRenderer>();    
    }

    void Start()
    {
        _blockSprite.color = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);    
    }

    public void OnHit()
    {
        hits--;

        if (hits <= 0)
        {
            GameController.Instance.AddScore(scoreValue);
            AudioController.Instance.PlayClip(OnBreakAudio);
            Instantiate(GameController.Instance.ExplosionFXPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
