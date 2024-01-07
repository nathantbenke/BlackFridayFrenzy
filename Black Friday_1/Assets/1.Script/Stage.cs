using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stage : MonoBehaviour
{


    public GenerateLevelAndShopList shoppingList;
    public List<PurchaseElement> purchaseList = new List<PurchaseElement>();

    //Obtain randomly among the items here 
    public List<string> itemNames = new List<string>();
    public List<int> purchaseListUI;

    public int count = 11;

    private void Start()
    {

        count = shoppingList.numOfItems;
        purchaseListUI = shoppingList.getPurchaseListUI();
        /*
        for (int i = 0; i < itemNames.Count; ++i)
        {
            int random1 = Random.Range(0, itemNames.Count);
            int random2 = Random.Range(0, itemNames.Count);

            string temp = itemNames[random1];
            itemNames[random1] = itemNames[random2];
            itemNames[random2] = temp;
        }
        // Can change how many random items we need to choose 
        for (int i = 0; i < count; i++) // 3 different items choose
        {
            PurchaseElement purchaseElement = new PurchaseElement();
            purchaseElement.itemName = itemNames[i];
            purchaseElement.count = Random.Range(2, count);
            purchaseList.Add(purchaseElement);
        }*/

        // the number of itemes to purchase

        // Debug.Log("count: " + count + " shoppingList.numOfItems: " + shoppingList.numOfItems);

        for (int i = 0; i < count; i++)
        {
            //Debug.Log("shoppingList.purchaseListUI.Count: " + shoppingList.purchaseListUI.Count);
            PurchaseElement purchaseElement = new PurchaseElement();
            //Debug.Log(shoppingList.purchaseListUI.IndexOf(i));
            purchaseElement.itemName = itemNames[purchaseListUI[i]];
            purchaseElement.count = 1;
            purchaseList.Add(purchaseElement);
        }
    }

    private void Update()
    {
        count = shoppingList.numOfItems;

    }


}