using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 8f;
    private float baseSpeed = 8f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Read the AD keys to move left and right
        float horizontalInput = 0;
        float verticalInput = 0;

        if (Input.GetKey(KeyCode.A))
        {
            horizontalInput = -0.5f;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            horizontalInput = 0.5f;
        }

        if (Input.GetKey(KeyCode.W))
        {
            verticalInput = 1.5f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            verticalInput = -1.5f;
        }

        // Read the shift key to move faster
        if (Input.GetKey(KeyCode.LeftShift))
        {
            speed = baseSpeed * 2;
        }
        else
        {
            speed = baseSpeed;
        }

        // Create a movement vector
        Vector3 movement = new Vector3(horizontalInput, 0, verticalInput);

        // Apply movement to the ball
        transform.position += movement * speed * Time.deltaTime;
    }
}