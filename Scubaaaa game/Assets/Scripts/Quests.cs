using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quests : MonoBehaviour
{
    public QuestType Quest;
    public GameObject Questpanel;


    public enum QuestType
    {
        BoatClean,
        FindItem,
        bouychain,
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


