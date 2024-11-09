using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] GameObject pauseMenuUI;
    [SerializeField] GameObject settingsMenuUI;
    [SerializeField] GameObject helpBoxUI;
    [SerializeField] GameObject mask;

    public bool isPaused = false;
    WaitForSecondsRealtime wait;

    private void Start()
    {
        wait = new WaitForSecondsRealtime(.1f);
    }

    public void Resume()
    {
        ResumeWithDelay();
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        AudioManager.instance.Play("Pause");
        PauseWithDelay();
        Time.timeScale = 0f;
    }

    private IEnumerator DelayToShowHide(GameObject gameObject, bool isActive)
    {
        yield return new WaitForSecondsRealtime(.1f);
        gameObject.SetActive(isActive);
    }


    public void PauseWithDelay()
    {
        StartCoroutine(DelayToPause());
    }

    public void ResumeWithDelay()
    {
        StartCoroutine(DelayToResume());
    }

    private IEnumerator DelayToPause()
    {
        yield return wait;
        player.GetComponent<PlayerInput>().enabled = false;
        player.GetComponent<PlayerMovement>().enabled = false;
        player.GetComponent<Animator>().enabled = false;
        mask.SetActive(true);
        pauseMenuUI.SetActive(true);
    }

    private IEnumerator DelayToResume()
    {
        yield return wait;
        mask.SetActive(false);
        pauseMenuUI.SetActive(false);
        settingsMenuUI.SetActive(false);
        helpBoxUI.SetActive(false);
        player.GetComponent<PlayerMovement>().enabled = true;
        player.GetComponent<PlayerInput>().enabled = true;
        player.GetComponent<Animator>().enabled = true;
    }

}
