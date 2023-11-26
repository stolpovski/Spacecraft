using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] Thruster bottomLeftThruster;
    [SerializeField] Thruster bottomRightThruster;
    [SerializeField] Thruster rightThruster;
    
    Rigidbody rb;
    Controls controls;

    void InitControls()
    {
        controls = new Controls();

        controls.Thrusters.Bottom.started += context =>
        {
            bottomLeftThruster.TurnOn();
            bottomRightThruster.TurnOn();
        };
        
        controls.Thrusters.Bottom.canceled += context =>
        {
            bottomLeftThruster.TurnOff();
            bottomRightThruster.TurnOff();
        };

        controls.Thrusters.Left.started += context =>
        {
            rightThruster.TurnOn();
        };

        controls.Thrusters.Left.canceled += context =>
        {
            rightThruster.TurnOff();
        };

        controls.Thrusters.Enable();
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
        InitControls();
    }
    
    void FixedUpdate()
    {
        rb.AddForceAtPosition(bottomLeftThruster.GetForce(), bottomLeftThruster.GetPosition(), ForceMode.Impulse);
        rb.AddForceAtPosition(bottomRightThruster.GetForce(), bottomRightThruster.GetPosition(), ForceMode.Impulse);
        rb.AddForceAtPosition(rightThruster.GetForce(), rightThruster.GetPosition(), ForceMode.Impulse);
    }
}
