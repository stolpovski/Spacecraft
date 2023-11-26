using System.Collections;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    [SerializeField] float maxForce = 0.01f;
    [SerializeField] float deltaForce = 0.0001f;

    float force;
    bool isRunning;
    ParticleSystem vfx;
    AudioSource sfx;

    private void Awake()
    {
        vfx = GetComponent<ParticleSystem>();
        sfx = GetComponent<AudioSource>();
    }

    public Vector3 GetForce()
    {
        return transform.rotation * Vector3.back * force;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
    
    public void TurnOn()
    {
        vfx.Play();
        sfx.Play();
        isRunning = true;
        StartCoroutine(ForceUp());
    }

    public void TurnOff()
    {
        vfx.Stop();
        vfx.Clear();
        sfx.Stop();
        isRunning = false;
        StartCoroutine(ForceDown());
    }

    IEnumerator ForceUp()
    {
        while (isRunning && force < maxForce)
        {
            force += deltaForce;
            yield return null;
        }
    }

    IEnumerator ForceDown()
    {
        while (!isRunning && force > 0)
        {
            force -= deltaForce;
            yield return null;
        }
    }
}
