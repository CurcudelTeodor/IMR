using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AnimateHandOnInput : MonoBehaviour
{
    public Animator handAnimator;

    // Update is called once per frame
    void Update()
    {
        float pinchInput = 0f;
        float gripInput = 0f;

        // Determine if this is the left or right hand based on the object's name
        if (gameObject.name.Contains("Left"))
        {
            // Simulate pinch animation with 'Z' key
            pinchInput = Input.GetKey(KeyCode.Z) ? 1.0f : 0.0f;

            // Simulate grip animation with 'G' key
            gripInput = Input.GetKey(KeyCode.G) ? 1.0f : 0.0f;
        }
        else if (gameObject.name.Contains("Right"))
        {
            // Simulate pinch animation with 'M' key
            pinchInput = Input.GetKey(KeyCode.M) ? 1.0f : 0.0f;

            // Simulate grip animation with 'G' key
            gripInput = Input.GetKey(KeyCode.G) ? 1.0f : 0.0f;
        }

        handAnimator.SetFloat("Trigger", pinchInput);
        handAnimator.SetFloat("Grip", gripInput);
    }
}
