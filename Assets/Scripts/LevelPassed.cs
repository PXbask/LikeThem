using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LevelPassed
{

    public string Name { get; set; }
    public float TotalTime { get; set; } = 0f;
    public float TimerLeft { get; set; } = 0;
    public int Attempts { get; set; } = 1;
    public int Gems { get; set; } = 0;
    public int Coins { get; set; } = 0;
    public bool FoundSecretArea { get; set; } = false;

}
