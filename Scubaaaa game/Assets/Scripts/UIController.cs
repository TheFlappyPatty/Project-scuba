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
    [SerializeField]
    private Canvas canvas;
    [SerializeField]
    private GameObject hand;
    public TextMeshProUGUI Popup;
    [SerializeField]
    private Texture EmptieHand;

    //list of all the UIHands in order
    [SerializeField]
    private List<GameObject> UIHandElements;



    public float UIspacing;
    //finds the type of ui your adding for display only
    public void AddUIElement(UItype f)
    {
        if(UItype.Hand == f)
        {
        UIHandElements.Add(Instantiate(hand,canvas.transform));
            FormateUIHands();
        }
        if(UItype.Quest == f)
        {

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

    public enum UItype {
    Hand,
    Quest,
    }

}
