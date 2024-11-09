using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.InputSystem;

public class LevelWinBoard : MonoBehaviour
{
    [SerializeField] float gemsAnimationDelay;
    [SerializeField] float coinsAnimationDelay;
    [SerializeField] GameObject gemsBoxUI;
    [SerializeField] GameObject coinBoxUI;
    [SerializeField] AudioMixer audioMixer;

    LevelPassed currentLevelInfo;
    TextMeshProUGUI coinScore;
    Animator coinScoreAnimator;
    LevelLoader levelLoader;
    MenuButtonController menuButtonController;
    PlayerInput playerInput;
    LevelInfo currentLevelManager;

    WaitForSecondsRealtime waitCoins;
    WaitForSecondsRealtime waitGems;

    float originalMusicLevel;

    int tempCoins = 0;
    int totalGemsOnMap = 3;

    private void Start()
    {
        AudioManager.instance.Play("LevelPassed");
        currentLevelManager = FindObjectOfType<LevelInfo>();
        currentLevelManager.DisableUI();
        currentLevelInfo = GlobalGameInfo.Instance.CurrentLevel;
        coinScore = coinBoxUI.GetComponentInChildren<TextMeshProUGUI>();
        coinScoreAnimator = coinBoxUI.GetComponentInChildren<Animator>();
        levelLoader = FindObjectOfType<LevelLoader>();
        waitGems = new WaitForSecondsRealtime(gemsAnimationDelay);
        waitCoins = new WaitForSecondsRealtime(coinsAnimationDelay);
        menuButtonController = GetComponent<MenuButtonController>();
        playerInput = GetComponent<PlayerInput>();
        //TogglePlayerInputAndButtonController(false);
        StartCoroutine(AnimateScore());
    }

    private void TogglePlayerInputAndButtonController(bool active)
    {
        playerInput.enabled = active;
        menuButtonController.enabled = active;
    }

    private IEnumerator AnimateScore()
    {
        for (int i = 0; i < currentLevelInfo.Coins; i++)
        {
            yield return waitCoins;
            AudioManager.instance.Play("Coin");
            IncrementScore();
        }

        if (currentLevelInfo.Gems > 0)
        {
            for (int i = 0; i < currentLevelInfo.Gems; i++)
            {
                yield return waitGems;
                AudioManager.instance.Play("TinyGem");
                StoreGemOnUI(i);
            }
            if (currentLevelInfo.Gems == totalGemsOnMap)
            {
                yield return waitGems;
                AudioManager.instance.Play("Gem");
                CollectedAllGemsAnimation();
            }
        }

        yield return waitGems;
        TogglePlayerInputAndButtonController(true);
    }

    public void LevelPassedAndLoadNext()
    {
        GlobalGameInfo.Instance.LevelPassed();
        levelLoader.LoadNextScene();
    }

    public void RestartLevel()
    {
        levelLoader.RestartScene();
    }

    public void MainMenu()
    {
        levelLoader.LoadMainMenu();
    }

    private void IncrementScore()
    {
        tempCoins++;
        coinScoreAnimator.SetTrigger("collect");
        UpdateCoinScoreDisplay();
    }

    private void UpdateCoinScoreDisplay()
    {
        coinScore.text = tempCoins.ToString();
    }

    public void StoreGemOnUI(int index)
    {
        gemsBoxUI.transform.GetChild(index).GetComponent<Animator>().SetTrigger("collect");
    }

    public void CollectedAllGemsAnimation()
    {
        for (int i = 0; i < totalGemsOnMap; i++)
        {
            gemsBoxUI.transform.GetChild(i).GetComponent<Animator>().SetTrigger("collectAll");
        }
    }
}
