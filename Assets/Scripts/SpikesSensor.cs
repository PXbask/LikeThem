using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesSensor : MonoBehaviour
{
    [SerializeField] float timeToDeactivate = 1.25f;

    Animator animator;

    private void Start()
    {
        animator = GetComponentInParent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.GetComponent<Player>())
        {
            return;
        }

        animator.SetBool("isActivated", true);
        StartCoroutine(ScheduleDeactivation());
    }

    private IEnumerator ScheduleDeactivation()
    {
        yield return new WaitForSecondsRealtime(timeToDeactivate);
        animator.SetBool("isActivated", false);
    }
}
