using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] AxisThrusters pitchThrusters;
    [SerializeField] AxisThrusters rollThrusters;
    [SerializeField] AxisThrusters yawThrusters;
    
    Rigidbody rb;
    Controls controls;

    void InitControls()
    {
        controls = new Controls();

        controls.Spacecraft.Pitch.started += context => StartThrusters(pitchThrusters, context.ReadValue<float>());
        controls.Spacecraft.Pitch.canceled += context => StopThrusters(pitchThrusters);

        controls.Spacecraft.Roll.started += context => StartThrusters(rollThrusters, context.ReadValue<float>());
        controls.Spacecraft.Roll.canceled += context => StopThrusters(rollThrusters);

        controls.Spacecraft.Yaw.started += context => StartThrusters(yawThrusters, context.ReadValue<float>());
        controls.Spacecraft.Yaw.canceled += context => StopThrusters(yawThrusters);

        controls.Spacecraft.Enable();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        InitControls();
    }

    void StartThrusters(AxisThrusters thrusters, float direction)
    {
        Debug.Log("started " + direction);
        if (direction > 0)
        {
            foreach (var thruster in thrusters.positive)
            {
                thruster.TurnOn();
            }
        }

        if (direction < 0)
        {
            foreach (var thruster in thrusters.negative)
            {
                thruster.TurnOn();
            }
        }
    }

    void StopThrusters(AxisThrusters thrusters)
    {
        Debug.Log("stopped");
        foreach (var thruster in thrusters.positive)
        {
            thruster.TurnOff();
        }

        foreach (var thruster in thrusters.negative)
        {
            thruster.TurnOff();
        }
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
        AddForces(rollThrusters);
        AddForces(yawThrusters);
    }
}
