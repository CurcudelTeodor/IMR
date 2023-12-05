using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHandsOnInput : MonoBehaviour
{
    public float rotationSpeed = 90f; // Adjust the rotation speed as needed
    public GameObject leftHand;
    public GameObject rightHand;

    // Update is called once per frame
    void Update()
    {
        // Rotate left hand with 'T' key
        if (Input.GetKey(KeyCode.T))
        {
            RotateHand(leftHand, -rotationSpeed * Time.deltaTime);
        }

        // Rotate right hand with 'Y' key
        if (Input.GetKey(KeyCode.Y))
        {
            RotateHand(rightHand, rotationSpeed * Time.deltaTime);
        }
    }

    void RotateHand(GameObject hand, float angle)
    {
        // Rotate the hand around its up axis
        hand.transform.Rotate(Vector3.up, angle);
    }
}
