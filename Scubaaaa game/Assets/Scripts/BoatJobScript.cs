using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatJobScript : MonoBehaviour
{
    public string BoatName = "Default Boat";
    public GameObject BoatToClean;
    public float  BoatCompletion = 0;
    public int Docknumber = 0;
    public string Description = "Describe the back story of the boat or boat Details";
    public bool boatTimer = false;
    public UIController UI;

    public void Start()
    {

    }
    public void StartJob()
    {
        UI.AddUIElement(UIController.UItype.Quest,BoatToClean);
    }
}
