using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerControler : MonoBehaviour
{
    [SerializeField]
    private int dominatehand;
    [SerializeField]
    private List<GameObject> Playerhands;




    //Players Current Function Stats vvvvvv
    [Header("Cleaning tools")]
    public float PlayerScraperSize = 10;
    [Range(0,1)]
    public float PlayerScraperStrength = 1;
    //Players Current Function Stats ^^^^^^^

    //all the UI tools
    [Space()]
    [Header("UI handeler")]
    private UIController UIhandler;
    [SerializeField]
    public LayerMask interactableObjects;

    //player movement stats
    [Space]
    [Header("Player stats")]
    [Range(10,1f)]
    public float sliprisistants;
    [Range(100, 10f)]
    public float WaterSlipRisistants;
    public float movementSpeed = 100;
    public float SwimSpeed = 20;
    public float MaxSpeedOnground = 8;
    public float MaxSpeedSwimming = 2;
    public float MaxAirSpeed = 20;
    public float Sensitivity = 0.5f;
    private Rigidbody PlayerBody;
    public GameObject Camera;
    public float Jumpforce;
    //player states
    [SerializeField]
    private PlayerState playerState = PlayerState.InAir;
    private PlayerState PreviuseState;


    private float X = 0;
    private float Y = 0;


    private enum PlayerState
    {
        Inwater,
        Onground,
        InAir,
        Paused,


    }

    private void Start()
    {
        UIhandler = GameObject.Find("UI").GetComponent<UIController>();
        PlayerBody = gameObject.GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        InitialiseHands();
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) UIhandler.syncLaptop();





        //Finds inputs for items
       for(int i = 0; i <=9; i++)
       {
            if (Input.GetKeyDown(KeyCode.Alpha0 + i))
            {
                if(i <= Playerhands.Count)
                { 
                    dominatehand = i - 1;
                    for(int f = 0; f <= Playerhands.Count -1; f++) 
                    {
                        if (Playerhands[f] != null) if (Playerhands[f] != Playerhands[dominatehand]) { Playerhands[f].SetActive(false); } else { Playerhands[f].SetActive(true); }
                    }
                }
                else
                {
                    return;
                }
            }
       }


       //This Handles item pickups and interactions vvvvvvv
        UIhandler.Popup.SetText("");
        if(playerState != PlayerState.Paused)
        {
            Ray interact = new Ray(Camera.transform.position, Camera.transform.forward);
            RaycastHit Target = new RaycastHit();
            if (Physics.Raycast(interact, out Target, 20f, interactableObjects))
            {
                if (Target.collider.tag == "Holdable")
                {

                    UIhandler.Popup.SetText("Press f to Pickup " + Target.transform.name);
                    if (Input.GetKeyDown(KeyCode.F) && Playerhands[dominatehand] == null)
                    {
                        PickupItem(Target.collider.gameObject, dominatehand);
                    }
                }
                //for Cleaning what you Looked at
                if (Target.collider.tag == "Cleanable")
                {
                    UIhandler.Popup.SetText("left Click to clean");
                    if (Input.GetKey(KeyCode.Mouse0))
                    {
                        Target.transform.GetComponent<CleanableObject>().CleanAt(Target.textureCoord, PlayerScraperSize, PlayerScraperStrength * Time.deltaTime);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.G))
            {
                DropItem();
            }
            //This Handles item pickups and interactions ^^^^^^^^^
        }




















        if(playerState != PlayerState.Paused)
        {
            //finds if the player is on the ground.
            if (playerState != PlayerState.Inwater && playerState != PlayerState.Paused)
            {
                Ray ray = new Ray(transform.position, Vector3.down);
                RaycastHit ground = new RaycastHit();
                if (Physics.Raycast(ray, out ground, 1.1f))
                {
                    playerState = PlayerState.Onground;
                }
                else
                {
                    playerState = PlayerState.InAir;
                }
            }
        }
        //Mouse Controles and cursor lock
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if(playerState == PlayerState.Paused)
            {
               playerState = PreviuseState;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Locked;
            }
            else
            {
                PreviuseState = playerState;
                playerState = PlayerState.Paused;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;
            }
        }


        //Player movement and direction out of water
        if (playerState != PlayerState.Paused)
        {
            X += Input.GetAxis("Mouse X") * Sensitivity;
            Y += Input.GetAxis("Mouse Y") * Sensitivity;
            Y = Mathf.Clamp(Y, -80, 90);
            Camera.transform.rotation = Quaternion.Euler(-Y, X, 0);


            if (playerState != PlayerState.Inwater)
            {
                Vector3 PlayerForward = new Vector3(Camera.transform.forward.normalized.x, 0, Camera.transform.forward.normalized.z);
                Vector3 PlayerRight = new Vector3(Camera.transform.right.normalized.x, 0, Camera.transform.right.normalized.z);
                if (PlayerBody.velocity.magnitude > 1)
                {
                    PlayerBody.velocity -= new Vector3(PlayerBody.velocity.normalized.x / sliprisistants,0, PlayerBody.velocity.normalized.z / sliprisistants);
                }
                if (Input.GetKeyDown(KeyCode.Space) && playerState == PlayerState.Onground)
                {
                    PlayerBody.AddForce(Vector3.up * Jumpforce, ForceMode.VelocityChange);
                }
                PlayerBody.AddForce(PlayerForward * Input.GetAxis("Vertical") * movementSpeed, ForceMode.Force);
                PlayerBody.AddForce(PlayerRight * Input.GetAxis("Horizontal") * movementSpeed, ForceMode.Force);
            }
            else //this is when the player is in water.
            {
                Vector3 PlayerForward = new Vector3(Camera.transform.forward.normalized.x, Camera.transform.forward.normalized.y, Camera.transform.forward.normalized.z);
                Vector3 PlayerRight = new Vector3(Camera.transform.right.normalized.x, Camera.transform.right.normalized.y, Camera.transform.right.normalized.z);
                PlayerBody.AddForce(Vector3.up * SwimSpeed * Input.GetAxis("Jump"), ForceMode.Force);
                PlayerBody.AddForce(Camera.transform.forward.normalized * Input.GetAxis("Vertical") * SwimSpeed, ForceMode.Force);
                PlayerBody.AddForce(Camera.transform.right.normalized * Input.GetAxis("Horizontal") * SwimSpeed, ForceMode.Force);
                if (PlayerBody.velocity.magnitude > 0)
                {
                    PlayerBody.velocity -= new Vector3(PlayerBody.velocity.normalized.x / WaterSlipRisistants, PlayerBody.velocity.normalized.y / WaterSlipRisistants, PlayerBody.velocity.normalized.z / WaterSlipRisistants);
                }
            }
        }


        //speed caps
        var x = PlayerBody.velocity.normalized.x;
        var z = PlayerBody.velocity.normalized.z;
        switch (playerState)
        {
            default:
                if (PlayerBody.velocity.magnitude > MaxSpeedOnground)
                {
                    PlayerBody.velocity = new Vector3(x,PlayerBody.velocity.y/MaxSpeedOnground,z) * MaxSpeedOnground;
                    Debug.Log("walking");
                }
                break;
            case PlayerState.InAir:
                if (PlayerBody.velocity.magnitude > MaxAirSpeed)
                {
                    PlayerBody.velocity = new Vector3(x,PlayerBody.velocity.y/MaxAirSpeed,z) * MaxAirSpeed;
                    Debug.Log("falling");
                }
                break;
            case PlayerState.Inwater:
                if (PlayerBody.velocity.magnitude > MaxSpeedSwimming)
                {
                    PlayerBody.velocity = PlayerBody.velocity.normalized * MaxSpeedSwimming;
                    Debug.Log("swimming");
                }
                break;
        }

    }



    public void PickupItem(GameObject Item,int Slot)
    {
        Playerhands[Slot] = Item;
        Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        Item.GetComponent<Collider>().enabled = false;
        UIhandler.ReadHandUI(Slot, Item.GetComponent<ItemScript>().ItemIcon);
        Item.transform.rotation = Camera.transform.rotation;
        float dis = 0.5f;
        Item.transform.position = new Vector3(Camera.transform.position.x + Camera.transform.forward.normalized.x *dis,Camera.transform.position.y -0.5f + Camera.transform.forward.normalized.y * dis,Camera.transform.position.z + Camera.transform.forward.normalized.z * dis);
        Item.transform.parent = Camera.transform;
    }
    public void DropItem()
    {
        GameObject Item = Playerhands[dominatehand];
        UIhandler.ReadHandUI(dominatehand, null);
        Item.transform.parent = null;
        Item.GetComponent<Collider>().enabled = enabled;
        Item.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        Playerhands[dominatehand] = null;
    }






    //finds is the player is in water or not
    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Water")
            {
            playerState = PlayerState.Inwater;
            PlayerBody.useGravity = false;
            }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.tag == "Water")
        {
            playerState = PlayerState.InAir;
            PlayerBody.useGravity = true;
        }
    }

    //used to add the Ui to the screen and link to player
    public void InitialiseHands()
    {
        foreach(GameObject n in Playerhands)
        {
           UIhandler.AddUIElement(UIController.UItype.Hand,null);
        }
    }
}
