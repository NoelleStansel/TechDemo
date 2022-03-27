using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    float mouseX;
    float mouseY;
    float xRotation = 0f;
    float sensMod;

    public float Sensitivity = 1000f;
    public Transform PlayerTrans;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        sensMod = 1;
    }

    void Update()
    {
        if (true) //here we can put a game over condition if we need one, artifact from previous code
        {
            mouseX = Input.GetAxis("Mouse X") * Sensitivity * sensMod * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse Y") * Sensitivity * sensMod * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);

            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            PlayerTrans.Rotate(Vector3.up * mouseX);    
        }
    }

    public void DisableLooking()
    {
        sensMod = 0;
    }
    public void EnableLooking()
    {
        sensMod = 1;
    }
}