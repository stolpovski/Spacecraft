using System.Collections;
using UnityEngine;

public class Thruster : Engine
{
    [SerializeField] float maxForce = 0.01f;
    [SerializeField] float deltaForce = 0.0001f;
    
    ParticleSystem vfx;
    AudioSource sfx;

    private void Awake()
    {
        vfx = GetComponent<ParticleSystem>();
        sfx = GetComponent<AudioSource>();
    }

    public override void TurnOn()
    {
        vfx.Play();
        sfx.Play();
        IsRunning = true;
        StartCoroutine(ForceUp());
    }

    public override void TurnOff()
    {
        vfx.Stop();
        vfx.Clear();
        sfx.Stop();
        IsRunning = false;
        StartCoroutine(ForceDown());
    }

    IEnumerator ForceUp()
    {
        while (IsRunning && force < maxForce)
        {
            force += deltaForce;
            yield return null;
        }
    }

    IEnumerator ForceDown()
    {
        while (!IsRunning && force > 0)
        {
            force -= deltaForce;
            yield return null;
        }
    }
}
