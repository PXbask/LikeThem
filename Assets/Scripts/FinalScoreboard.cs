using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FinalScoreboard : MonoBehaviour
{
    [SerializeField] GameObject levelBoxInfoPrefab;
    [SerializeField] Sprite gemCollectedSprite;
    [SerializeField] int maxColumns = 4;


    GameObject[,] levelsPassedBoard;
    int currentBoardLine = 0, lastLineIndex = 0, levelsPassedCount;

    void Start()
    {
        levelsPassedCount = GlobalGameInfo.Instance.LevelsPassed.Count;
        levelsPassedBoard = new GameObject[20, maxColumns];
        lastLineIndex = (int)levelsPassedCount / maxColumns;

        //if (GlobalGameInfo.Instance.LevelsPassed.Count <= 0) return;

        for (int i = 0; i < levelsPassedCount; i++)
        {
            GameObject levelInfoBox = Instantiate(levelBoxInfoPrefab, new Vector2(0, (55 - 45*(i%4))), transform.rotation) as GameObject;
            levelInfoBox.transform.SetParent(transform.Find("Score Button"), false);

            levelInfoBox.transform.Find("Name")
                .GetComponent<TextMeshProUGUI>()
                .text = $"Level {i + 1}";

            levelInfoBox.transform.Find("Total Time")
                .GetComponent<TextMeshProUGUI>()
                .text = $"Total: {GlobalGameInfo.Instance.LevelsPassed[i].TotalTime}s";

            levelInfoBox.transform.Find("Timer Left")
                .GetComponentInChildren<TextMeshProUGUI>()
                .text = GlobalGameInfo.Instance.LevelsPassed[i].TimerLeft.ToString();

            levelInfoBox.transform.Find("Attempts")
                .GetComponent<TextMeshProUGUI>()
                .text = $"Attempts: {GlobalGameInfo.Instance.LevelsPassed[i].Attempts}";


            levelInfoBox.transform.Find("Coins")
                .GetComponentInChildren<TextMeshProUGUI>()
                .text = GlobalGameInfo.Instance.LevelsPassed[i].Coins.ToString();

            levelInfoBox.transform.Find("Secret Area Toggle")
                .GetComponentInChildren<Toggle>()
                .isOn = GlobalGameInfo.Instance.LevelsPassed[i].FoundSecretArea;


            ChangeGemSpriteToCollected(GlobalGameInfo.Instance.LevelsPassed[i].Gems, levelInfoBox);

            if (i < maxColumns) levelInfoBox.SetActive(true);

            levelsPassedBoard[(i / maxColumns), (i % maxColumns)] = levelInfoBox;
        }
    }

    private void ChangeGemSpriteToCollected(int totalGems, GameObject levelInfoBox)
    {
        if (totalGems > 0)
        {
            for (int gem = 0; gem < totalGems; gem++)
            {
                StoreGemOnUI(gem, levelInfoBox.transform.Find("Gems"));
            }
        }
    }

    public void PreviousBoard()
    {
        if (levelsPassedCount <= maxColumns)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");
        SetActiveCurrentLine(false);

        if (currentBoardLine == 0)
        {
            currentBoardLine = (int) Mathf.Ceil(lastLineIndex);
            SetActiveCurrentLine(true);
        }
        else
        {
            currentBoardLine--;
            SetActiveCurrentLine(true);
        }
    }

    public void NextBoard()
    {
        if (levelsPassedCount <= maxColumns)
        {
            AudioManager.instance.Play("Error");
            return;
        }

        AudioManager.instance.Play("Select");
        SetActiveCurrentLine(false);

        if (currentBoardLine < maxColumns - 1)
        {
            currentBoardLine++;
            SetActiveCurrentLine(true);
        }
        else
        {
            currentBoardLine = 0;
            SetActiveCurrentLine(true);
        }
    }


    private void SetActiveCurrentLine(bool active)
    {
        var lastColumnIndex = maxColumns;
        if (currentBoardLine == lastLineIndex)
        {
            lastColumnIndex = levelsPassedCount % maxColumns;
        }

        for (int i = 0; i < lastColumnIndex; i++) levelsPassedBoard[currentBoardLine, i].SetActive(active);
    }

    public void StoreGemOnUI(int gemIndex, Transform gemBox)
    {
        gemBox.GetChild(gemIndex).GetComponent<Image>().sprite = gemCollectedSprite;
    }

    public void CollectedAllGemsAnimation(Transform gemBox)
    {
        for (int i = 0; i < 3; i++)
        {
            gemBox.GetChild(i).GetComponent<Animator>().SetTrigger("collectAll");
        }
    }
}
