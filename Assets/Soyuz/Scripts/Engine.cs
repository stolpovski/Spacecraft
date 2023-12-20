using UnityEngine;

public abstract class Engine : MonoBehaviour, IThrustable
{
    protected float force;
    bool isRunning;

    public bool IsRunning
    {
        get => isRunning;
        set => isRunning = value;
    }

    public Vector3 Force => transform.rotation * Vector3.back * force;

    public Vector3 Position => transform.position;

    public abstract void TurnOn();
    public abstract void TurnOff();
}
