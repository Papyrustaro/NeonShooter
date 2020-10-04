using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlePlayOnEnable : MonoBehaviour
{
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        _particleSystem.time = 0f;
        _particleSystem.Play(true);
    }
}
