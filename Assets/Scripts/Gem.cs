using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.LWRP;
using UnityEngine.Experimental.Rendering.Universal;

public class Gem : MonoBehaviour
{
    [SerializeField] GameObject sparklesVFX;
    LevelInfo levelInfo;

    private void Start()
    {
        levelInfo = FindObjectOfType<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player")) return;

        Pickup();
    }

    private void Pickup()
    {
        AudioManager.instance.Play("Gem");
        Instantiate(sparklesVFX, transform.position, transform.rotation);
        levelInfo.StoreGemOnUI();
        Destroy(gameObject);
    }
}
