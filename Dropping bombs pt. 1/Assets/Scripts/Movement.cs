using UnityEngine;

public class Movement : MonoBehaviour
{

    [Header("Movement")]
    [Tooltip("Speed of movement")]
    public float speed;
    
    // Fixed Update is a frame-rate independent method called at fixed intervals
    // to allow for consistent physics calculations
    private void FixedUpdate()
    {
        // Gets user input
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Creates a vector for movement using the user input
        Vector3 movement = new(horizontal, vertical);

        // Applies this vector to the object
        transform.position += movement * (Time.deltaTime * speed);
    }

}

