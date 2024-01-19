using JetBrains.Annotations;
using System.Collections;
using UnityEngine;
using EZCameraShake;

public class ShakeController : MonoBehaviour
{
    public Camera playerCamera;
    public Transform targetObject; // Assign the target object in the Unity Editor
    private Vector3 originalCameraPosition;
    public float shakeDuration;
    public float shakeMagnitude;

    void Update()
    {
        originalCameraPosition = playerCamera.transform.localPosition;
        // Check if the distance between the camera and the target object is less than 2 meters
        float distanceToTarget = Vector3.Distance(playerCamera.transform.position, targetObject.position);

        if (distanceToTarget < 2f)
        {
            // Add camera shake when attacking
            //StartCoroutine(cameraShake.Shake(.15f, .1f));
            CameraShaker.Instance.ShakeOnce(1f, 1f, 0.1f, 1f);

        }
    }

    IEnumerator CameraShake(float duration, float magnitude)
    {
        Debug.Log("Im shaking the camera");
        float elapsed = 0.0f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            // Apply the shake offset to the camera's position relative to its current position
            playerCamera.transform.localPosition += new Vector3(x, y, 0);

            elapsed += Time.deltaTime;

            yield return null;
        }

        // Reset the camera position to the original position after the shake
        playerCamera.transform.localPosition = originalCameraPosition;
    }
}
