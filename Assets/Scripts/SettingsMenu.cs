using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SettingsMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI difficultyText;
    [SerializeField] TextMeshProUGUI currentScreenText;
    [SerializeField] TextMeshProUGUI languageText;
    [SerializeField] GameObject brightnessVolumeBox; 
    [SerializeField] GameObject sfxVolumeBox; 
    [SerializeField] GameObject musicVolumeBox;

    GlobalInfo globalInfo;

    Color whiteTransparent = new Color(255f, 255f, 255f, .5f);
    Color white = new Color(255f, 255f, 255f, 1f);

    void Start()
    {
        globalInfo = GlobalGameInfo.Instance.GetComponent<GlobalInfo>();
        ApplySettingsToMenu();
    }

    public void ApplyDefaultSettings()
    {
        globalInfo.ApplyDefaultSettings();
        ApplySettingsToMenu();
    }

    private void ApplySettingsToMenu()
    {
        difficultyText.text = globalInfo.difficulties[globalInfo.currentDificultyIndex];
        currentScreenText.text = globalInfo.screenModes[globalInfo.currentScreenModeIndex];
        languageText.text = globalInfo.languages[globalInfo.currentLanguageIndex];

        PaintBoxVolume(brightnessVolumeBox, globalInfo.currentBrightness);
        PaintBoxVolume(sfxVolumeBox, globalInfo.currentSFXVolume);
        PaintBoxVolume(musicVolumeBox, globalInfo.currentMusicVolume);
    }

    private void PaintBoxVolume(GameObject box, int volume)
    {
        for (int i = 0; i < volume; i++)
        {
            Paint(box, i);
        }

        for (int i = volume; i < 10; i++)
        {
            Unpaint(box, i);
        }
    }

    public void SetNextDifficulty()
    {
        SetNext("Difficulty");
        //TODO: Add chamada ao global info pra aplicar alteraçoes
    }

    public void SetPreviousDifficulty()
    {
        SetPrevious("Difficulty");
        //TODO: Add chamada ao global info pra aplicar alteraçoes
    }

    public void SetNextLanguage()
    {
        SetNext("Language");
        //TODO: Add chamada ao global info pra aplicar alteraçoes
    }

    public void SetPreviousLanguage()
    {
        SetPrevious("Language");
        //TODO: Add chamada ao global info pra aplicar alteraçoes
    }

    public void SetNextScreenMode()
    {
        SetNext("ScreenMode");
        globalInfo.SetScreenMode();
    }

    public void SetPreviousScreenMode()
    {
        SetPrevious("ScreenMode");
        globalInfo.SetScreenMode();
    }

    public void MinusBrigthness()
    {
        if (globalInfo.currentBrightness <= 0)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");

        Unpaint(brightnessVolumeBox, --globalInfo.currentBrightness);
        globalInfo.SetBrightness();
    }

    public void PlusBrightness()
    {
        if (globalInfo.currentBrightness >= 10)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");

        Paint(brightnessVolumeBox, globalInfo.currentBrightness++);
        globalInfo.SetBrightness();
    }

    public void MinusSFX()
    {
        if (globalInfo.currentSFXVolume <= 0)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");

        Unpaint(sfxVolumeBox, --globalInfo.currentSFXVolume);
        globalInfo.SetSFXVolume();
    }

    public void PlusSFX()
    {
        if (globalInfo.currentSFXVolume >= 10)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");

        Paint(sfxVolumeBox, globalInfo.currentSFXVolume++);
        globalInfo.SetSFXVolume();
    }

    public void MinusMusic()
    {
        if (globalInfo.currentMusicVolume <= 0)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");

        Unpaint(musicVolumeBox, --globalInfo.currentMusicVolume);
        globalInfo.SetBackgroundMusicVolume();
    }

    public void PlusMusic()
    {
        if (globalInfo.currentMusicVolume >= 10)
        {
            AudioManager.instance.Play("Error");
            return;
        }
        AudioManager.instance.Play("Select");

        Paint(musicVolumeBox, globalInfo.currentMusicVolume++);
        globalInfo.SetBackgroundMusicVolume();
    }

    private void Unpaint(GameObject gameObject, int index)
    {
        gameObject.transform.GetChild(index).GetComponent<RawImage>().color = whiteTransparent;
    }

    private void Paint(GameObject gameObject, int index)
    {
        gameObject.transform.GetChild(index).GetComponent<RawImage>().color = white;
    }


    private string SetNext(string settingName)
    {
        AudioManager.instance.Play("Select");

        switch (settingName)
        {
            case "Difficulty":
                globalInfo.currentDificultyIndex = GetValidIndex(globalInfo.difficulties, globalInfo.currentDificultyIndex + 1);
                var difficulty = globalInfo.difficulties[globalInfo.currentDificultyIndex];
                difficultyText.text = difficulty;

                return difficulty;

            case "ScreenMode":
                globalInfo.currentScreenModeIndex = GetValidIndex(globalInfo.screenModes, globalInfo.currentScreenModeIndex + 1);
                var screenMode = globalInfo.screenModes[globalInfo.currentScreenModeIndex];
                currentScreenText.text = screenMode;

                return screenMode;

            case "Language":
                globalInfo.currentLanguageIndex = GetValidIndex(globalInfo.languages, globalInfo.currentLanguageIndex + 1);
                var language = globalInfo.languages[globalInfo.currentLanguageIndex];
                languageText.text = language;

                return language;

            default:
                Debug.LogWarning("Setting " + settingName + " does not exists");
                return null;
        }
    }

    private string SetPrevious(string settingName)
    {
        AudioManager.instance.Play("Select");

        switch (settingName)
        {
            case "Difficulty":
                globalInfo.currentDificultyIndex = GetValidIndex(globalInfo.difficulties, globalInfo.currentDificultyIndex - 1);
                var difficulty = globalInfo.difficulties[globalInfo.currentDificultyIndex];
                difficultyText.text = difficulty;

                return difficulty;

            case "ScreenMode":
                globalInfo.currentScreenModeIndex = GetValidIndex(globalInfo.screenModes, globalInfo.currentScreenModeIndex - 1);
                var screenMode = globalInfo.screenModes[globalInfo.currentScreenModeIndex];
                currentScreenText.text = screenMode;

                return screenMode;

            case "Language":
                globalInfo.currentLanguageIndex = GetValidIndex(globalInfo.languages, globalInfo.currentLanguageIndex - 1);
                var language = globalInfo.languages[globalInfo.currentLanguageIndex];
                languageText.text = language;

                return language;

            default:
                Debug.LogWarning("Setting " + settingName + " does not exists");
                return null;
        }
    }

    private int GetValidIndex(string[] array, int index)
    {
        if (index >= array.Length) return 0;
        else if (index < 0) return array.Length - 1;
        else return index;
    }
}
