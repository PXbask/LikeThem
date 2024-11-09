using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public enum Type
{
    SFX, OST
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    public AudioMixerGroup audioMixerGroup;
    public bool loop;
    [HideInInspector] public AudioSource source;

    public Type Type
    {
        get
        {
            if (audioMixerGroup.name == AudioMixerGroupName.Music.ToString()) return Type.OST;
            else return Type.SFX;
        }
    }

    public void Initialize(AudioSource audioSource)
    {
        source = audioSource;
        source.outputAudioMixerGroup = audioMixerGroup;
        source.clip = clip;
        source.loop = loop;
    }

}
