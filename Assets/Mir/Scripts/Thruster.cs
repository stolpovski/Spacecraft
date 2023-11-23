using System.Collections;
using UnityEngine;

public class Thruster : MonoBehaviour
{
    bool isRunning;
    float force;
    float maxForce = 0.01f;
    float deltaForce = 0.0001f;

    public float GetForce()
    {
        return force;
    }
    
    public void TurnOn()
    {
        isRunning = true;
        StartCoroutine(ForceUp());
    }

    public void TurnOff()
    {
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
