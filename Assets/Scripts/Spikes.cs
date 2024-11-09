using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    public void PlaySliceSFX()
    {
        AudioManager.instance.Play("Spikes");
    }
}
