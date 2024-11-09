using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAnimationFrame : MonoBehaviour
{
    [SerializeField] string animationName;
    [SerializeField] int totalFrames;

    void Start() => GetComponent<Animator>().Play(animationName, 0, Random.Range(0f, totalFrames));
}
