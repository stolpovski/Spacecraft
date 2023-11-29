using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainEngine : MonoBehaviour
{
    public float fuel;
    public bool isFiring;
    public float maxThrust = 100f;
    public float thrust;

    public Vector3 GetForce()
    {
        return transform.rotation * Vector3.back * thrust;
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void TurnOn()
    {
        isFiring = true;
        thrust = maxThrust;
    }

    public void TurnOff()
    {
        isFiring = false;
        thrust = 0;
    }

    public void Toggle()
    {
        if (isFiring)
        {
            TurnOff();
        } else
        {
            TurnOn();
        }
    }
}
