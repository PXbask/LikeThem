using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.InputSystem;
using Quaternion = UnityEngine.Quaternion;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Player : MonoBehaviour
{
    [SerializeField] int life = 1;
    //[SerializeField] float forceBack = 3f;

    Animator animator;
    CameraShake cameraShake;
    PlayerMovement playerMovement;
    bool alreadyCollided = false;


    void Start()
    {
        animator = GetComponent<Animator>();
        cameraShake = FindObjectOfType<CameraShake>();
        playerMovement = GetComponent<PlayerMovement>();
        Debug.Log(GetComponent<PlayerInput>().devices);
    }

    public void DealDamage(int amount)
    {
        if (alreadyCollided) return;

        alreadyCollided = true;
        playerMovement.Freeze();

        Die();
        cameraShake.PlayShake();
        GlobalGameInfo.Instance.CurrentLevel.Attempts++;
    }

    public void Die()
    {
        playerMovement.Freeze();
        playerMovement.enabled = false;
        Vibrate(0.234f);
        cameraShake.PlayShake();
        //playerMovement.ForceBack(forceBack);
        animator.SetTrigger("death");
        AudioManager.instance.Play("Death");
        FindObjectOfType<LevelInfo>().StopCountdown();
        StartCoroutine(TransitionThenRestartLevel());
    }

    private IEnumerator TransitionThenRestartLevel()
    {
        yield return new WaitForSeconds(1f);
        StopVibrating();
        FindObjectOfType<LevelLoader>().RestartScene();
    }

    public void SetActiveWithDelay(bool isActive)
    {
        StartCoroutine(DelayToShowHide(isActive));
    }

    private IEnumerator DelayToShowHide(bool isActive)
    {
        yield return new WaitForSecondsRealtime(1f);
        gameObject.SetActive(isActive);
        if (isActive)
        {
            GetComponent<PlayerInput>().ActivateInput();
        }
        else
        {
            GetComponent<PlayerInput>().DeactivateInput();
        }
    }

    public void Vibrate(float value)
    {
        Debug.Log(GetComponent<PlayerInput>().devices);
        //if (GetComponent<PlayerInput>().devices == )
        //{

        //}
        //Gamepad.current.SetMotorSpeeds(value, value);
    }

    public void StopVibrating()
    {
        //Gamepad.current.SetMotorSpeeds(0f, 0f);
    }
}
