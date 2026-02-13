using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public QuestType Quest;


    public enum QuestType
    {
        BoatClean,
        FindItem,
        bouychain,
    }
    public void BoatCleaner(string BoatName, float BoatCompletion, int Docknumber, string Description, bool boatTimer, float Timerlenght, float timerLenght)
    {
        BoatName = "Default Boat";
        BoatCompletion = 0;
        Docknumber = 0;
        Description = "Describe the back story of the boat or boat Details";
        boatTimer = false;
    }


    public void LostItem(GameObject MissingItem, GameObject ReturnPoint, string Description, bool Timer)
    {
        Description = "Describe the missing item";
        Timer = false;
    }


    public void Bouychain(GameObject chainDestination, string Description, bool Timer, float timerLength)
    {
        Description = "about the job";
    }
}


