using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{
    public List<PurchaseElement> purchaseList = new List<PurchaseElement>();

    //Obtain randomly among the items here 
    public List<string> itemNames = new List<string>();

    public int count = 3; // how many items the player have to pick and bring

    private void Awake()
    {
        for (int i = 0; i < itemNames.Count; ++i)
        {
            int random1 = Random.Range(0, itemNames.Count);
            int random2 = Random.Range(0, itemNames.Count);

            string temp = itemNames[random1];
            itemNames[random1] = itemNames[random2];
            itemNames[random2] = temp;
        }
        // Can change how many random items we need to choose 
        for (int i = 0; i < 3; i++) // 3 different items choose
        {
            PurchaseElement purchaseElement = new PurchaseElement();
            purchaseElement.itemName = itemNames[i];
            purchaseElement.count = Random.Range(2, count);
            purchaseList.Add(purchaseElement);
        }
    }
}

