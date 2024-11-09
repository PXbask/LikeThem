using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    [SerializeField] Animator transitionAnimator;
    [SerializeField] float transitionTime = 1f;
    int currentSceneIndex;


    public void ResetGlobalGameInfo() => GlobalGameInfo.Instance.ResetAllGameInfo();

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void LoadScene(int index)
    {
        GlobalGameInfo.Instance.ResetCurrentLevelInfo();
        StartCoroutine(LoadLevel(index));
    }

    public void LoadNextScene()
    {
        GlobalGameInfo.Instance.ResetCurrentLevelInfo();
        StartCoroutine(LoadLevel(currentSceneIndex + 1));
    }

    public void RestartScene()
    {
        GlobalGameInfo.Instance.ResetCurrentLevelInfoBesideAttempts();
        StartCoroutine(LoadLevel(currentSceneIndex));
    }

    public void LoadMainMenu()
    {
        GlobalGameInfo.Instance.ResetAllGameInfo();
        StartCoroutine(LoadLevel(0));
    }

    public void QuitGame()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    private IEnumerator LoadLevel(int index)
    {
        Time.timeScale = 1f;
        transitionAnimator.SetTrigger("start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(index);
    }
}
