using UnityEngine;

public interface IThrustable
{
    Vector3 Force { get; }
    Vector3 Position { get; }
    void TurnOn();
    void TurnOff();
}