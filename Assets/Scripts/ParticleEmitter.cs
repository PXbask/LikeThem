using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEmitter : MonoBehaviour
{
    [SerializeField] float lifeTime = .2f;

    ParticleSystem particles;

    private void Start()
    {
        particles = GetComponent<ParticleSystem>();
        Emit();
    }

    private void Emit()
    {
        StartCoroutine(StopEmission());
    }

    private IEnumerator StopEmission()
    {
        yield return new WaitForSeconds(lifeTime);
        particles.enableEmission = false;
    }
}
