using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trackanduntrack : MonoBehaviour
{
    public GameObject HudData;
    public GameObject trackbutton;
    public GameObject untrackbutton;

    public void Start()
    {
        if (FindObjectOfType<UIController>().activeQuests.Contains(HudData))
        {
            trackbutton.SetActive(false);
            untrackbutton.SetActive(true);
        }
        else
        {
            trackbutton.SetActive(true);
            untrackbutton.SetActive(false);
        }
    }

    public void Untrack()
    {
        FindObjectOfType<UIController>().UntrackQuest(gameObject);
    }
public void Track()
    {
        FindObjectOfType<UIController>().trackQuest(gameObject);
    }
}
