using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    public float movementSpeed;
    public float Sensitivity = 0.5f;
    public Rigidbody PlayerBody;
    public GameObject Camera;
    public float Jumpforce;

    private float X = 0;
    private float Y = 0;

    private void Start()
    {
        PlayerBody = gameObject.GetComponent<Rigidbody>();
            Cursor.lockState = CursorLockMode.Locked;
    }

    public void Update()
    {
        Y = Mathf.Clamp(Y,-80, 90);
        X += Input.GetAxis("Mouse X") * Sensitivity;
        Y += Input.GetAxis("Mouse Y") * Sensitivity;
        Camera.transform.rotation = Quaternion.Euler(-Y,X,0);
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }


            PlayerBody.AddForce(-Input.GetAxis("Horizontal") * movementSpeed, 0,-Input.GetAxis("Vertical") * movementSpeed);

    }

}
