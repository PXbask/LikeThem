using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] float duration = 0.5f;
    [SerializeField] float speed = 5.0f;
    [SerializeField] float magnitude = 0.2f;

    CameraFollow cameraFollow;

    private void Start()
    {
        cameraFollow = GetComponent<CameraFollow>();
    }

    //This function is used outside (or inside) the script
    public void PlayShake()
    {
        //StopAllCoroutines();
        StartCoroutine("Shake");
    }



    private IEnumerator Shake()
    {
        cameraFollow.enabled = false;

        float elapsed = 0.0f;

        Vector3 originalCamPos = transform.position;

        float randomStart = Random.Range(-1000.0f, 1000.0f);

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;

            float percentComplete = elapsed / duration;

            float damper = 1.0f - Mathf.Clamp(1.5f * percentComplete - 1.0f, 0.0f, 1.0f);
            float alpha = randomStart + speed * percentComplete;

            float x = Mathf.PerlinNoise(alpha, 0.0f) * 2.0f - 1.0f;
            float y = Mathf.PerlinNoise(0.0f, alpha) * 2.0f - 1.0f;



            x *= magnitude * damper;
            y *= magnitude * damper;

            x += originalCamPos.x;
            y += originalCamPos.y;


            transform.position = new Vector3(x, y, originalCamPos.z);

            yield return 0;
        }

        transform.position = Vector3.Lerp(transform.position, originalCamPos, Time.deltaTime * 5f);
    }
}
