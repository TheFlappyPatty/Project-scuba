using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{



    //UI Objects
    public List<GameObject> InactiveQuests;
    public List<GameObject> activeQuests;
    public int ActiveQuestLimit = 6;

    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject hand;
    [SerializeField]
    private GameObject BoatQuest;
    [SerializeField]
    private GameObject LaptopboatUI;
    public TextMeshProUGUI Popup;
    [SerializeField]
    private Texture EmptieHand;
    public laptop Laptop;

    //list of all the UIHands in order
    [SerializeField]
    private List<GameObject> UIHandElements;



    public float UIspacing;
    //finds the type of ui your adding for display only
    public void AddUIElement(UItype f,GameObject g)
    {
        if(UItype.Hand == f)
        {
        UIHandElements.Add(Instantiate(hand,canvas.transform));
            FormateUIHands();
        }
        if(UItype.Quest == f && g != null)
        {
            if (ActiveQuestLimit >= activeQuests.Count)
            {
                activeQuests.Add(Instantiate(BoatQuest,canvas.transform));
                syncLaptop();
                FormateUIQuest();
            } else
            {
                InactiveQuests.Add(Instantiate(BoatQuest, canvas.transform));
            }

        }

    }

    public void ReadHandUI(int index,Texture f)
    {
        UIHandElements[index].GetComponentInChildren<RawImage>().texture = f;
        if(f = null)
        {
            UIHandElements[index].GetComponentInChildren<RawImage>().texture = EmptieHand;
        }
    }


    //only use if downgrading should not be needed
    public void RemoveHandElement(GameObject n,int f)
    {
        if(n != null)
        {
        UIHandElements.Remove(n);
        }
        else
        {
            UIHandElements.RemoveAt(f);
        }
    }

    //rearranges the ui on screen to fix problems with overlap
    void FormateUIHands()
    {
        int count = 0;
        foreach(GameObject hand in UIHandElements)
        {
            hand.GetComponent<RectTransform>().anchoredPosition = new Vector2(-775 + 100 * count,-420);
            count++;
        }
    }
    void FormateUIQuest()
    {
        int count = 0;
        foreach(GameObject objective in activeQuests)
        {
            objective.GetComponent<RectTransform>().anchoredPosition = new Vector2(742,275 - 100 * count);
            count++;
        }
    }
   public void syncLaptop()
    {
        foreach (GameObject f in Laptop.ActiveQuestsinlist)
        {
            Destroy(f);
        }
        foreach (GameObject h in Laptop.InactiveQuestinlist)
        {
            Destroy(h);
        }
        Laptop.InactiveQuestinlist.Clear();
        Laptop.ActiveQuestsinlist.Clear();
        foreach (GameObject  r in InactiveQuests)
        {
            Laptop.InactiveQuestinlist.Add(r.GetComponent<JobDataHUD>().LaptopUIhud = Instantiate(LaptopboatUI, Laptop.InActiveQuestlist.transform));
            r.GetComponent<JobDataHUD>().Refresh();
        }
        foreach (GameObject g in activeQuests)
        {
            Laptop.ActiveQuestsinlist.Add(g.GetComponent<JobDataHUD>().LaptopUIhud = Instantiate(LaptopboatUI, Laptop.ActiveQuestlist.transform));
            g.GetComponent<JobDataHUD>().Refresh();
        }
        FormateActiveQuestList();
        FormateInactiveQuestlist();
    }

    void FormateActiveQuestList()
    {
        int count = 0;
        foreach(GameObject f in Laptop.ActiveQuestsinlist)
        {
            f.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 111.2f - 100 * count, 0);
            Debug.Log("formated");
            count++;
        }

    }
    void FormateInactiveQuestlist()
    {
        int count = 0;
        foreach(GameObject f in Laptop.InactiveQuestinlist)
        {
            f.GetComponent<RectTransform>().anchoredPosition = new Vector3(0, 111.2f - 100 * count, 0);
            Debug.Log("formated");
            count++;
        }
    }


    public void UntrackQuest(GameObject Quest)
    {
        activeQuests.Remove(Quest.GetComponent<Trackanduntrack>().HudData);
        InactiveQuests.Add(Quest.GetComponent<Trackanduntrack>().HudData);
        syncLaptop();
    }
    public void trackQuest(GameObject Quest)
    {
        InactiveQuests.Remove(Quest.GetComponent<Trackanduntrack>().HudData);
        activeQuests.Add(Quest.GetComponent<Trackanduntrack>().HudData);
        syncLaptop();
    }


    public enum UItype {
    Hand,
    Quest,
    }

}
