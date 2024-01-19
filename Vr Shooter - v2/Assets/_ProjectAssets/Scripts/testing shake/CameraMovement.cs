using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float movementSpeed = 5f; // Adjust this value to control the movement speed
    public float rotationSpeed = 2f; // Adjust this value to control the rotation speed

    void Update()
    {
        // Get input from the keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate the movement direction
        Vector3 movement = new Vector3(horizontalInput, 0f, verticalInput) * movementSpeed * Time.deltaTime;

        // Move the camera
        transform.Translate(movement);

        // Get mouse input for rotation
        float mouseX = Input.GetAxis("Mouse X");

        // Rotate the camera based on mouse input
        transform.Rotate(Vector3.up, mouseX * rotationSpeed);
    }
}
