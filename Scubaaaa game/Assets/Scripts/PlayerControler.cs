using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour
{
    private float movementSpeed = 100;
    [Range(10,0)]
    public float sliprisistants;
    public float MaxSpeed;
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
        //Mouse Controles and cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }
        X += Input.GetAxis("Mouse X") * Sensitivity;
        Y += Input.GetAxis("Mouse Y") * Sensitivity;
        Y = Mathf.Clamp(Y,-80, 90);
        Camera.transform.rotation = Quaternion.Euler(-Y,X,0);

        //Player movement and direction
        Vector3 PlayerForward = new Vector3(Camera.transform.forward.normalized.x,0,Camera.transform.forward.normalized.z);
        Vector3 PlayerRight = new Vector3(Camera.transform.right.normalized.x, 0, Camera.transform.right.normalized.z);
        PlayerBody.AddForce(PlayerForward * Input.GetAxis("Vertical") * movementSpeed,ForceMode.Force);
        PlayerBody.AddForce(PlayerRight * Input.GetAxis("Horizontal") * movementSpeed,ForceMode.Force);
        if (PlayerBody.velocity.magnitude > MaxSpeed)
        {
            PlayerBody.velocity = PlayerBody.velocity.normalized * MaxSpeed;
        }

    }

}
