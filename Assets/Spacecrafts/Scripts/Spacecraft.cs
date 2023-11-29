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

    private void Update()
    {
        Debug.Log(rb.velocity.magnitude);
    }

    public void OnMainThrust(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            mainEngine.Toggle();
        }
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
            thrusters.TurnOfRunningThrusters();
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

    void AddForces(AxisThrusters thrusters)
    {
        foreach (var thruster in thrusters.positive)
        {
            rb.AddForceAtPosition(thruster.GetForce(), thruster.GetPosition(), ForceMode.Impulse);
        }

        foreach (var thruster in thrusters.negative)
        {
            rb.AddForceAtPosition(thruster.GetForce(), thruster.GetPosition(), ForceMode.Impulse);
        }
    }

    void FixedUpdate()
    {
        AddForces(pitchThrusters);
        AddForces(yawThrusters);
        AddForces(rollThrusters);
        rb.AddForceAtPosition(mainEngine.GetForce(), mainEngine.GetPosition(), ForceMode.Impulse);
    }
}
