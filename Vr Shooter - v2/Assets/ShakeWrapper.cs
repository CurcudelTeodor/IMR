// Moving the ShakeWrapper -> NOT WORKING
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeWrapper : MonoBehaviour
{
    private int recoilDistance = 5;
    public void ApplyRecoil()
    {
        StartCoroutine(RecoilRoutine());
    }

    IEnumerator RecoilRoutine()
    {   
        Debug.Log("Am AJUNS AICI");
        //move the wrapper back
        Vector3 originalPosition = transform.position;
        Vector3 recoilPosition = originalPosition - transform.forward * recoilDistance;
        Debug.Log("orginal below");
        Debug.Log(originalPosition);
        
        Debug.Log("recoil below");
        Debug.Log(recoilPosition);
        
        float elapsedTime = 0f;
        float duration = 0.8f;

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
// Moving the GameObject (parent) -> NOT WORKING
// using System.Collections;
// using UnityEngine;

// public class ShakeWrapper : MonoBehaviour
// {
//     private int recoilDistance = 5;

//     public void ApplyRecoil()
//     {
//         StartCoroutine(RecoilRoutine());
//     }

//     IEnumerator RecoilRoutine()
//     {
//         Transform parentTransform = transform.parent;  // Get the parent transform
//         if (parentTransform == null)
//         {
//             Debug.LogError("ShakeWrapper is not a child of any GameObject.");
//             yield break;
//         }

//         Vector3 originalPosition = parentTransform.position;
//         Vector3 recoilPosition = originalPosition - parentTransform.forward * recoilDistance;

//         float elapsedTime = 0f;
//         float duration = 0.2f;

//         while (elapsedTime < duration)
//         {
//             parentTransform.position = Vector3.Lerp(originalPosition, recoilPosition, elapsedTime / duration);
//             elapsedTime += Time.deltaTime;
//             yield return null;
//         }

//         // Reset position after recoil
//         parentTransform.position = originalPosition;
//     }
// }
