using UnityEngine;

public abstract class Engine : MonoBehaviour, IThrustable
{
    protected float force;
    bool isRunning;

    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }

    public Vector3 Force
    {
        get { return transform.rotation * Vector3.back * force; }
    }

    public Vector3 Position
    {
        get { return transform.position; }
    }

    public abstract void TurnOn();
    public abstract void TurnOff();
}
