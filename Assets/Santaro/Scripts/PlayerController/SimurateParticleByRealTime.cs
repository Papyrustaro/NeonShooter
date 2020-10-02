using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// timeScale=0でもparticleを動かす
/// </summary>
public class SimurateParticleByRealTime : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particleSystem;
    private void Update()
    {
        _particleSystem.Simulate(Time.unscaledDeltaTime, withChildren: true, restart: false);
    }
}
