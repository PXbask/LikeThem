using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalGameInfo : Singleton<GlobalGameInfo>
{
    public List<LevelPassed> LevelsPassed { get; private set; }
    public LevelPassed CurrentLevel { get; set; }

    void Start()
    {
        LevelsPassed = new List<LevelPassed>();
        CurrentLevel = new LevelPassed();
    }

    public void LevelPassed()
    {
        CurrentLevel.Name = SceneManager.GetActiveScene().name;
        LevelsPassed.Add(CurrentLevel);
    }

    public void ResetAllGameInfo()
    {
        LevelsPassed = new List<LevelPassed>();
        ResetCurrentLevelInfo();
    }

    public void ResetCurrentLevelInfo() => CurrentLevel = new LevelPassed();

    public int ResetCurrentLevelInfoBesideAttempts()
    {
        var currentLevel = new LevelPassed
        {
            Attempts = ++CurrentLevel.Attempts
        };

        CurrentLevel = currentLevel;

        return CurrentLevel.Attempts;
    }
}
