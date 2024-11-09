using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] GameObject laserPrefab;
    [SerializeField] GameObject laserHitPrefab;
    [SerializeField] float minTimeForActivation = 3f;
    [SerializeField] float maxTimeForActivation = 4f;

    GameObject laserHit;

    WaitForSeconds waitForSeconds;
    float originOffset = .51f;
    // Start is called before the first frame update
    void Start()
    {
        waitForSeconds = new WaitForSeconds(UnityEngine.Random.Range(minTimeForActivation, maxTimeForActivation));
        Vector3 originPosition = new Vector3(transform.position.x + originOffset, transform.position.y, 0f);
        Vector3 direction = transform.TransformDirection(new Vector3(0, 1, 0));

        RaycastHit2D hitInfo = Physics2D.Raycast(originPosition, direction, LayerMask.GetMask("Foreground"));
        Debug.DrawRay(originPosition, direction, Color.red);
        float distance = (float)Math.Ceiling(hitInfo.distance);

        CreateLaserLine(distance);

    }

    private IEnumerator ScheduleLaserActivation(GameObject laser)
    {
        while (true)
        {
            yield return waitForSeconds;
            laser.GetComponent<Animator>().SetTrigger("activated");
        }
    }

    private void CreateLaserLine(float distance)
    {
        StartCoroutine(ScheduleLaserMineActivation());

        for (float i = originOffset; i < distance; i++)
        {
            Vector3 laserPos = new Vector3(transform.position.x + originOffset + i, transform.position.y, 0f);
            GameObject laser = Instantiate(laserPrefab, laserPos, transform.rotation);
            laser.transform.parent = transform;
            StartCoroutine(ScheduleLaserActivation(laser));
        }

        CreateInactiveLaserHit(distance);
    }

    private IEnumerator ScheduleLaserMineActivation()
    {
        while (true)
        {
            yield return waitForSeconds;

            GetComponent<Animator>().SetTrigger("activated");
        }
    }

    private void CreateInactiveLaserHit(float distance)
    {
        GameObject laserHit = Instantiate(laserHitPrefab, new Vector3(transform.position.x + distance, transform.position.y, 0f), transform.rotation);
        laserHit.transform.parent = transform;
        this.laserHit = laserHit;
    }

    public void ActivateLaserHit()
    {
        laserHit.GetComponent<Animator>().SetTrigger("activated");
    }
}
