using UnityEngine;
using UnityEngine.InputSystem;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] Thruster bottomLeftThruster;
    [SerializeField] Thruster topLeftThruster;
    [SerializeField] Thruster bottomRightThruster;
    [SerializeField] Thruster topRightThruster;
    [SerializeField] Thruster rightDownThruster;
    [SerializeField] Thruster rightUpThruster;
    [SerializeField] Thruster leftUpThruster;
    [SerializeField] Thruster leftDownThruster;
    
    Rigidbody rb;
    Controls controls;

    void InitControls()
    {
        controls = new Controls();

        controls.Spacecraft.Pitch.started += StartPitch;
        controls.Spacecraft.Pitch.canceled += StopPitch;

        controls.Spacecraft.RollLeft.started += context =>
        {
            rightDownThruster.TurnOn();
            leftUpThruster.TurnOn();
        };

        controls.Spacecraft.RollLeft.canceled += context =>
        {
            rightDownThruster.TurnOff();
            leftUpThruster.TurnOff();
        };

        controls.Spacecraft.RollRight.started += context =>
        {
            rightUpThruster.TurnOn();
            leftDownThruster.TurnOn();
        };

        controls.Spacecraft.RollRight.canceled += context =>
        {
            rightUpThruster.TurnOff();
            leftDownThruster.TurnOff();
        };

        controls.Spacecraft.Yaw.started += StartYaw;
        controls.Spacecraft.Yaw.canceled += StopYaw;

        controls.Spacecraft.Enable();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
        InitControls();
    }
    
    void FixedUpdate()
    {
        Pitch();
        Roll();
        Yaw();
    }

    void AddForce(Thruster thruster)
    {
        rb.AddForceAtPosition(thruster.GetForce(), thruster.GetPosition(), ForceMode.Impulse);
    }

    void Pitch()
    {
        AddForce(bottomLeftThruster);
        AddForce(bottomRightThruster);
        AddForce(topLeftThruster);
        AddForce(topRightThruster);
    }

    void Roll()
    {
        AddForce(rightDownThruster);
        AddForce(leftUpThruster);
        AddForce(rightUpThruster);
        AddForce(leftDownThruster);
    }

    void Yaw()
    {

    }

    void StartPitch(InputAction.CallbackContext context)
    {
        float pitch = context.ReadValue<float>();

        if (pitch > 0)
        {
            bottomLeftThruster.TurnOn();
            bottomRightThruster.TurnOn();
        }

        if (pitch < 0)
        {
            topLeftThruster.TurnOn();
            topRightThruster.TurnOn();
        }
    }

    void StopPitch(InputAction.CallbackContext context)
    {
        bottomLeftThruster.TurnOff();
        bottomRightThruster.TurnOff();
        topLeftThruster.TurnOff();
        topRightThruster.TurnOff();
    }

    void StartYaw(InputAction.CallbackContext context)
    {
        Debug.Log("Yaw started" + context.ReadValue<float>());
    }

    void StopYaw(InputAction.CallbackContext context)
    {
        Debug.Log("Yaw stopped");
    }
}
