using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] GameObject levelWinBoard;
    LevelInfo levelInfo;
    Animator animator;
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
        player = FindObjectOfType<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        AudioManager.instance.Play("Door");
        levelInfo = FindObjectOfType<LevelInfo>();

        levelInfo.StopCountdown();
        levelInfo.StopLevelTimer();

        StopBouncingAnimation();
        StartCoroutine(DisablePlayerAndOpenDoor());
    }

    private IEnumerator DisablePlayerAndOpenDoor()
    {
        yield return new WaitForSeconds(.2f);
        player.gameObject.SetActive(false);
        yield return new WaitForSeconds(.3f);
        levelWinBoard.SetActive(true);
    }

    public void StopBouncingAnimation()
    {
        animator.SetBool("isOpen", false);
    }
}
