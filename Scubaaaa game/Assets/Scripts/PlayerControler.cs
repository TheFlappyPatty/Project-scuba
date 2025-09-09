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
    }

    public void Update()
    {

        X += Input.GetAxis("Mouse X") * Sensitivity;

        Y += Input.GetAxis("Mouse Y") * Sensitivity;
        Camera.transform.rotation =  new Quaternion(0,X,Y,0);
            PlayerBody.AddForce(-Input.GetAxis("Horizontal") * movementSpeed, 0,-Input.GetAxis("Vertical") * movementSpeed);

    }

}
