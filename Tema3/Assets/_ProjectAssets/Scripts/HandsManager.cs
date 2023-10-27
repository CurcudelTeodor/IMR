using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandsManager : MonoBehaviour
{
    [SerializeField]
    Animator leftHand;
    [SerializeField]
    Animator rightHand;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("space") && Input.GetKey(KeyCode.G))
        {
            rightHand.ResetTrigger("isThrowing");
            rightHand.SetTrigger("isGrabbing");
        }
        else if(Input.GetKeyUp("space") || Input.GetKeyUp(KeyCode.G))
        {
            rightHand.SetTrigger("isThrowing");
            rightHand.ResetTrigger("isGrabbing");
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.G))
        {
            leftHand.ResetTrigger("isThrowing");
            leftHand.SetTrigger("isGrabbing");
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.G))
        {
            leftHand.SetTrigger("isThrowing");
            leftHand.ResetTrigger("isGrabbing");
        }
    }
}
