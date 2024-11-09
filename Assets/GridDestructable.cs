using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDestructable : MonoBehaviour
{
    [SerializeField] GameObject breakingVFX;

    Animator animator;
    CameraShake cameraShake;
    LevelLoader levelLoader;

    int hitCount;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        cameraShake = FindObjectOfType<CameraShake>();
        StartCoroutine(CameraUnfollow());
        levelLoader = FindObjectOfType<LevelLoader>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hitCount++;
        animator.SetTrigger("hit");
        cameraShake.PlayShake();
        AudioManager.instance.Play("DestroyGrid");
        Instantiate(breakingVFX, transform.position, transform.rotation);
        FindObjectOfType<PlayerMovement>().Move(Vector2.right);
        if (hitCount > 1)
        {
            AudioManager.instance.Play("SecretDoor");
            StartCoroutine(LoadNextLevel());
        }
    }

    private IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(.7f);
        levelLoader.LoadNextScene();
    }

    IEnumerator CameraUnfollow()
    {
        yield return new WaitForSeconds(.1f);
        cameraShake.gameObject.GetComponent<CameraFollow>().enabled = false;
    }
}
