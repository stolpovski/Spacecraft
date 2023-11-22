using UnityEngine;

public class Spacecraft : MonoBehaviour
{
    [SerializeField] Transform e1;
    [SerializeField] float force;
    Rigidbody rb;
    float vertical;
    float horizontal;
    float rotation;
    float smoothTime = 10f;
    float yVelocity = 0.0f;
    float targetForce;
    float t;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    

    private void Update()
    {
        vertical = Input.GetAxis("Vertical");
        horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.W))
        {
            targetForce = force;
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            targetForce = 0f;
        }

        


        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (targetForce > 0 && rotation < targetForce)
        {
            rotation += 0.00001f;
        }

        if (targetForce == 0 && rotation > targetForce)
        {
            rotation -= 0.00001f;
        }
        
        Debug.Log(rotation);


        rb.AddForceAtPosition(e1.rotation * Vector3.back * rotation, e1.position, ForceMode.Impulse);

    }
}
