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

    [SerializeField] Thruster leftTopThruster;
    [SerializeField] Thruster leftBottomThruster;
    [SerializeField] Thruster rightTopThruster;
    [SerializeField] Thruster rightBottomThruster;
    
    Rigidbody rb;
    Controls controls;

    void InitControls()
    {
        controls = new Controls();

        controls.Spacecraft.Pitch.started += StartPitch;
        controls.Spacecraft.Pitch.canceled += StopPitch;

        controls.Spacecraft.Roll.started += StartRoll;
        controls.Spacecraft.Roll.canceled += StopRoll;

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
        AddForce(leftTopThruster);
        AddForce(leftBottomThruster);
        AddForce(rightTopThruster);
        AddForce(rightBottomThruster);
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

    void StartRoll(InputAction.CallbackContext context)
    {
        float roll = context.ReadValue<float>();

        if (roll > 0)
        {
            rightUpThruster.TurnOn();
            leftDownThruster.TurnOn();
        }

        if (roll < 0)
        {
            rightDownThruster.TurnOn();
            leftUpThruster.TurnOn();
        }
    }

    void StopRoll(InputAction.CallbackContext context)
    {
        rightDownThruster.TurnOff();
        leftUpThruster.TurnOff();
        rightUpThruster.TurnOff();
        leftDownThruster.TurnOff();
    }

    void StartYaw(InputAction.CallbackContext context)
    {
        float yaw = context.ReadValue<float>();

        if (yaw > 0)
        {
            rightTopThruster.TurnOn();
            rightBottomThruster.TurnOn();
        }

        if (yaw < 0)
        {
            leftTopThruster.TurnOn();
            leftBottomThruster.TurnOn();
        }
    }

    void StopYaw(InputAction.CallbackContext context)
    {
        rightTopThruster.TurnOff();
        rightBottomThruster.TurnOff();
        leftTopThruster.TurnOff();
        leftBottomThruster.TurnOff();
    }
}
