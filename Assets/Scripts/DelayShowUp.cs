using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayShowUp : MonoBehaviour
{
    [SerializeField] float time = 1f;

    void OnEnable()
    {
        StartCoroutine(WaitToShowUp());
    }

    private IEnumerator WaitToShowUp()
    {
        yield return new WaitForSeconds(time);
        GetComponent<Animator>().enabled = true;
    }
}
