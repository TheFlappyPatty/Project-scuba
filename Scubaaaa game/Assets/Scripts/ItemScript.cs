using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemScript : MonoBehaviour
{
    private static Texture DefaultUI;
    public Texture ItemIcon;

    public void Start()
    {
        if(ItemIcon == null)
        {
            ItemIcon = DefaultUI;
        }
    }
}
