using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    public void Play()
    {
        _particle.Play();
    }
}
