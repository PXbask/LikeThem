using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Experimental.Rendering.Universal;

public class GlobalInfo : MonoBehaviour
{
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Light2D globalLight;

    [HideInInspector] public int currentDificultyIndex = 0;
    [HideInInspector] public int currentScreenModeIndex = 0;
    [HideInInspector] public int currentBrightness = 0;
    [HideInInspector] public int currentSFXVolume = 0;
    [HideInInspector] public int currentMusicVolume = 0;
    [HideInInspector] public int currentLanguageIndex = 0;

    private static readonly string FIRST_PLAY = "FirstPlay";
    private static readonly string DIFFICULTY = "DifficultyPref";
    private static readonly string SCREEN_MODE = "ScreenModePref";
    private static readonly string BRIGHTNESS = "BrightnessPref";
    private static readonly string SFX_VOLUME = "SFXVolumePref";
    private static readonly string MUSIC_VOLUME = "MusicVolumePref";
    private static readonly string LANGUAGE = "LanguagePref";
    private int firstPlay;

    [HideInInspector] public string[] difficulties;
    [HideInInspector] public string[] screenModes;
    [HideInInspector] public string[] languages;

    enum Difficulty
    {
        NORMAL, HARD, EASY
    }

    enum ScreenMode
    {
        WINDOWED, FULL_SCREEN
    }

    enum Language
    {
        ENGLISH, PORTUGUESE
    }


    private void Awake()
    {
        difficulties = Enum.GetNames(typeof(Difficulty));
        screenModes = Enum.GetNames(typeof(ScreenMode));
        languages = Enum.GetNames(typeof(Language));
    }

    void Start()
    {
        ApplyDefaultSettings();

        //firstPlay = PlayerPrefs.GetInt(FIRST_PLAY);

        //if (firstPlay == 0)
        //{
        //    ApplyDefaultSettings();
        //    PlayerPrefs.SetInt(FIRST_PLAY, -1);
        //}
        //else
        //{
        //    GetPlayerPrefs();
        //    ApplySettings();
        //}
    }

    private void OnApplicationQuit()
    {
        SavePlayerPrefs();
    }
    private void GetPlayerPrefs()
    {
        currentDificultyIndex = PlayerPrefs.GetInt(DIFFICULTY);
        currentScreenModeIndex = PlayerPrefs.GetInt(SCREEN_MODE);
        currentBrightness = PlayerPrefs.GetInt(BRIGHTNESS);
        currentSFXVolume = PlayerPrefs.GetInt(SFX_VOLUME);
        currentMusicVolume = PlayerPrefs.GetInt(MUSIC_VOLUME);
        currentLanguageIndex = PlayerPrefs.GetInt(LANGUAGE);
    }

    private void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt(DIFFICULTY, currentDificultyIndex);
        PlayerPrefs.SetInt(SCREEN_MODE, currentScreenModeIndex);
        PlayerPrefs.SetInt(BRIGHTNESS, currentBrightness);
        PlayerPrefs.SetInt(SFX_VOLUME, currentSFXVolume);
        PlayerPrefs.SetInt(MUSIC_VOLUME, currentMusicVolume);
        PlayerPrefs.SetInt(LANGUAGE, currentLanguageIndex);
    }

    private void ApplySettings()
    {
        //TODO: difficulty
        //TODO: language translation
        SetScreenMode();
        SetBrightness();
        SetSFXVolume();
        SetBackgroundMusicVolume();
    }

    public void ApplyDefaultSettings()
    {
        currentDificultyIndex = 0;
        currentScreenModeIndex = 0;
        currentBrightness = 5;
        currentSFXVolume = 9;
        currentMusicVolume = 7;
        currentLanguageIndex = 0;

        ApplySettings();
    }

    public void SetScreenMode()
    {
        if (screenModes[currentScreenModeIndex] == ScreenMode.FULL_SCREEN.ToString())
        {
            Screen.fullScreen = true;
            Screen.SetResolution(800, 600, true);
        }
        else
        {
            Screen.fullScreen = false;
            Screen.SetResolution(800, 600, false);
        }
    }

    public void SetBrightness()
    {
        //globalLight.intensity = .3f + (currentBrightness * .06f);
    }

    public void SetBackgroundMusicVolume()
    {
        var volume = currentMusicVolume * 8f - 80f;
        //SetGroupVolume("MusicVolume", Mathf.Log10(volume) * 20);
        SetGroupVolume("MusicVolume", volume);
    }

    public void SetSFXVolume()
    {
        var volume = currentSFXVolume * 8f - 80f;

        //SetGroupVolume("SFXVolume", Mathf.Log10(volume) * 20);
        SetGroupVolume("SFXVolume", volume);
    }
    private void SetGroupVolume(string groupName, float volume)
    {
        audioMixer.SetFloat(groupName, volume);
    }
}
