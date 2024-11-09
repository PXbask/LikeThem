using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField] Sound[] sounds;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound sound in sounds)
        {
            sound.Initialize(gameObject.AddComponent<AudioSource>());
        }
    }

    private void Start()
    {
        Play("Theme");
    }


    public void Play(string name)
    {
        Sound sound = Array.Find(sounds, s => s.name == name);

        if (sound == null)
        {
            Debug.LogWarning($"Sound {name} not found.");
        }

        if (sound.Type == Type.SFX)
        {
            sound.source.PlayOneShot(sound.clip);
        }
        else
        {
            sound.source.Play();
        }
    }
}
