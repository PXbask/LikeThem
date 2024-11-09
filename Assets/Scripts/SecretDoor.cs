using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDoor : MonoBehaviour
{
    [SerializeField] GameObject secretArea;
    [SerializeField] float timeBetweenGlitches = 3f;

    Animator secretAreaAnimator;
    Animator secretDoorAnimator;
    SpriteRenderer sprite;
    LevelInfo levelInfo;

    private bool isFirstTimeEntering = true;
    private bool isInside = false;

    private void Start()
    {
        secretAreaAnimator = secretArea.GetComponent<Animator>();
        secretDoorAnimator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        InvokeRepeating("Glitch", 1f, timeBetweenGlitches);
        levelInfo = FindObjectOfType<LevelInfo>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.CompareTag("Player"))
        {
            if (isFirstTimeEntering)
            {
                AudioManager.instance.Play("SecretDoor");
                isFirstTimeEntering = false;
                levelInfo.FoundSecretArea();
            }

            if (!isInside)
            {
                isInside = true;
                secretAreaAnimator.SetTrigger("fadeOut");
                sprite.enabled = false;
            }
            else
            {
                isInside = false;
                sprite.enabled = true;
                secretAreaAnimator.SetTrigger("fadeIn");
            }
        }
    }

    private void Glitch()
    {
        secretDoorAnimator.SetTrigger("glitch");
    }
}
