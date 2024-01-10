using System.Collections;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ShakeWrapper : MonoBehaviour
{
    private float recoilDistance = 0.05f;
    public Camera playerCamera;  // Reference to the player's camera

    public void ApplyRecoil()
    {
        StartCoroutine(RecoilRoutine());
    }

    IEnumerator RecoilRoutine()
    {
        Debug.Log("Am AJUNS AICI");

        if (playerCamera == null)
        {
            Debug.LogError("Player camera not found.");
            yield break;
        }

        // Move the wrapper back relative to the player's view
        Vector3 originalPosition = transform.position;
        Vector3 recoilPosition = originalPosition - playerCamera.transform.forward * recoilDistance;

        Debug.Log("orginal below");
        Debug.Log(originalPosition);

        Debug.Log("recoil below");
        Debug.Log(recoilPosition);

        float elapsedTime = 0f;
        float duration = 0.2f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            Debug.Log("Current Position: " + transform.position);
            yield return null;
        }

        // Reset position after recoil
        transform.position = originalPosition;
    }
}
