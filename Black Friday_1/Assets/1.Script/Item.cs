using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; // item name

    private void Start()
    {
        Market.Instance.AddItem(this);
    }
}
