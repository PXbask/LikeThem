using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] int points = 1;

    LevelInfo levelInfo;

    private void Start()
    {
        levelInfo = FindObjectOfType<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>()) return;

        Pickup();
    }

    private void Pickup()
    {
        AudioManager.instance.Play("Coin");
        levelInfo.AddToScore(points);
        Destroy(gameObject);
    }
}
