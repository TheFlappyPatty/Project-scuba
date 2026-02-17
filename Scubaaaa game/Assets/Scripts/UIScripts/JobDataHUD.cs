using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JobDataHUD : MonoBehaviour
{
    public string BoatDisplayName = "Default Boat";
    public GameObject LaptopUIhud;
    public float BoatCompletion = 0;
    public int Docknumber = 0;
    public string Description = "Describe the back story of the boat or boat Details";

   public void Refresh()
    {
        LaptopUIhud.GetComponent<Trackanduntrack>().HudData = gameObject;
    }


}
