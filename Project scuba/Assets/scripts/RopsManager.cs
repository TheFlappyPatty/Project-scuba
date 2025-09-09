using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopsManager : MonoBehaviour
{
    public GameObject player;
    public Transform ropeStart;
    public GameObject rope;
    public float Ropelength;
    public GameObject prevconected;

       void Start()
    {

        for (int i = 0; i < Ropelength; i++)
        {
            GameObject a = Instantiate(rope, ropeStart.position + new Vector3(0,-i/2,0), Quaternion.identity, ropeStart);
            if (prevconected != null) a.GetComponent<HingeJoint2D>().connectedBody = prevconected.GetComponent<Rigidbody2D>();
            prevconected = a;

        }
        player.GetComponent<HingeJoint2D>().connectedBody = prevconected.GetComponent<Rigidbody2D>();
    }

    public void addRope()
    {
        GameObject a = Instantiate(rope, ropeStart.position + new Vector3(0, prevconected.transform.position.y/3, 0), Quaternion.identity, ropeStart);
        if (prevconected != null) a.GetComponent<HingeJoint2D>().connectedBody = prevconected.GetComponent<Rigidbody2D>();
        prevconected = a;
    }



    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            addRope();
        }
    }
}
