using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] Thruster bottomThruster;
    Rigidbody rb;
    Controls controls;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        
        controls = new Controls();

        controls.Thrusters.Bottom.started += context => bottomThruster.TurnOn();
        controls.Thrusters.Bottom.canceled += context => bottomThruster.TurnOff();

        controls.Thrusters.Enable();
    }




    // Update is called once per frame
    void FixedUpdate()
    {

        rb.AddForceAtPosition(bottomThruster.transform.rotation * Vector3.back * bottomThruster.GetForce(), bottomThruster.transform.position, ForceMode.Impulse);

    }
}
