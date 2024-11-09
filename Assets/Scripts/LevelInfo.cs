using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelInfo : MonoBehaviour
{
    [SerializeField] int countdownInitialTimer = 12;
    [SerializeField] int totalGemsOnMap = 3;
    [SerializeField] GameObject keyUI;
    [SerializeField] GameObject gemsUI;
    [SerializeField] GameObject countDownUI;
    [SerializeField] GameObject coinScoreUI;

    TextMeshProUGUI timer;
    TextMeshProUGUI coinScore;
    Animator coinScoreAnimator;
    Animator timerAnimator;

    private void Start()
    {
        Debug.Log($"Total COINS: {FindObjectsOfType<CoinPickup>().Length}");
        countDownUI.gameObject.SetActive(false);
        timer = countDownUI.GetComponentInChildren<TextMeshProUGUI>();
        timerAnimator = countDownUI.GetComponentInChildren<Animator>();
        coinScore = coinScoreUI.GetComponentInChildren<TextMeshProUGUI>();
        coinScoreAnimator = coinScoreUI.GetComponentInChildren<Animator>();
        //UpdateCoinScoreDisplay();
        GlobalGameInfo.Instance.CurrentLevel.TimerLeft = countdownInitialTimer;
        StartLevelTimer();
    }

    public void AddToScore(int points)
    {
        GlobalGameInfo.Instance.CurrentLevel.Coins += points;
        coinScoreAnimator.SetTrigger("collect");
        UpdateCoinScoreDisplay();
    }

    private void UpdateCoinScoreDisplay()
    {
        coinScore.text = GlobalGameInfo.Instance.CurrentLevel.Coins.ToString();
    }


    public void StartCountdownTimer()
    {
        InvokeRepeating("Countdown", 1f, 1f);
    }

    public void StartLevelTimer()
    {
        InvokeRepeating("LevelTimer", 0f, 1);
    }

    void LevelTimer()
    {
        ++GlobalGameInfo.Instance.CurrentLevel.TotalTime;
    }

    void Countdown()
    {
        countDownUI.gameObject.SetActive(true);

        if (GlobalGameInfo.Instance.CurrentLevel.TimerLeft > 0)
        {
            GlobalGameInfo.Instance.CurrentLevel.TimerLeft--;
            timer.text = GlobalGameInfo.Instance.CurrentLevel.TimerLeft.ToString();
            AudioManager.instance.Play("Timer");
        }
        else
        {
            StopCountdown();
            FindObjectOfType<Player>().Die();
            timerAnimator.enabled = false;
            FindObjectOfType<Door>().StopBouncingAnimation();
        }
    }

    public void StoreKeyOnUI()
    {
        keyUI.GetComponent<Animator>().SetBool("gotKey", true);
    }

    public void StoreGemOnUI()
    {
        GlobalGameInfo.Instance.CurrentLevel.Gems++;

        if (GlobalGameInfo.Instance.CurrentLevel.Gems == totalGemsOnMap)
        {
            for (int i = 0; i < totalGemsOnMap; i++)
            {
                gemsUI.transform.GetChild(i).GetComponent<Animator>().SetTrigger("collectAll");
            }
        } else
        {
            gemsUI.transform.GetChild(GlobalGameInfo.Instance.CurrentLevel.Gems - 1).GetComponent<Animator>().SetTrigger("collect");
        }
    }

    public void FoundSecretArea()
    {
        GlobalGameInfo.Instance.CurrentLevel.FoundSecretArea = true;
    }

    public void StopCountdown()
    {
        CancelInvoke("Countdown");
        timerAnimator.enabled = false;
    }

    public void StopLevelTimer()
    {
        CancelInvoke("LevelTimer");
    }

    public void DisableUI()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActiveRecursively(false);
        }
    }
}
