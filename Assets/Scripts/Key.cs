using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] GameObject door;
    [SerializeField] GameObject pointer;
    LevelInfo levelInfo;

    private void Start()
    {
        levelInfo = FindObjectOfType<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Pickup();
        door.gameObject.SetActive(true);
    }

    private void Pickup()
    {
        AudioManager.instance.Play("Key");
        pointer.SetActive(true);
        levelInfo.StoreKeyOnUI();
        levelInfo.StartCountdownTimer();
        Destroy(gameObject);
    }
}
