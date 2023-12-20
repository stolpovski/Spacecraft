using UnityEngine;
using UnityEngine.InputSystem;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] MainEngine mainEngine;

    [SerializeField] AxisThrusters pitchThrusters;
    [SerializeField] AxisThrusters yawThrusters;
    [SerializeField] AxisThrusters rollThrusters;

    Rigidbody rb;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
    }

    void Rotate(AxisThrusters thrusters, float rotation)
    {
        if (rotation > 0)
        {
            thrusters.TurnOnPositiveThrusters();
        }

        if (rotation < 0)
        {
            thrusters.TurnOnNegativeThrusters();
        }

        if (rotation == 0)
        {
            thrusters.TurnOffRunningThrusters();
        }
    }

    public void OnMainThrust(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainEngine.Toggle();
        }
    }

    public void OnPitch(InputAction.CallbackContext context)
    {
        Rotate(pitchThrusters, context.ReadValue<float>());
    }

    public void OnYaw(InputAction.CallbackContext context)
    {
        Rotate(yawThrusters, context.ReadValue<float>());
    }

    public void OnRoll(InputAction.CallbackContext context)
    {
        Rotate(rollThrusters, context.ReadValue<float>());
    }

    void AddForce(IThrustable engine)
    {
        rb.AddForceAtPosition(engine.Force, engine.Position, ForceMode.Impulse);
    }

    void AddForces(AxisThrusters thrusters)
    {
        foreach (Thruster thruster in thrusters.positive)
        {
            AddForce(thruster);
        }

        foreach (Thruster thruster in thrusters.negative)
        {
            AddForce(thruster);
        }
    }

    void FixedUpdate()
    {
        AddForces(pitchThrusters);
        AddForces(yawThrusters);
        AddForces(rollThrusters);
        AddForce(mainEngine);
    }
}
