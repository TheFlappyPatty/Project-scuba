using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControler : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rig;
    public float speed;
    public float speedcap;
    void Start()
    {
        player = gameObject;
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rig.AddForce(new Vector2(Input.GetAxis("Horizontal"),Input.GetAxis("Vertical")));
        rig.AddForce(-rig.velocity /4 );
        if (rig.velocity.magnitude >= speedcap)
        {
            rig.AddForce(-rig.velocity);
        }
    }
}