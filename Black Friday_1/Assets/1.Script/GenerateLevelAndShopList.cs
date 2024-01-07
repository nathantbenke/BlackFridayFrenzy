using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GenerateLevelAndShopList : MonoBehaviour
{
    /*
     * @author Nathan Thomas-Benke
     */


    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;
    public GameObject item7;

    public GameObject shoppingSlotPrefab;

    private List<int> selectableItems = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8 };
    private int totalNumofItems = 7;

    private int levelNum;
    public int numOfItems;
    private int itemsToSelect;
    
    public List<int> purchaseListUI = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        levelNum = getLevelNumber();
        switch(levelNum)
        {
            case 1: //Level 1
                numOfItems = 3;
                break;
            case 2: //Level 2
                numOfItems = 5;
                break;
            case 3: //Level 3
                numOfItems = 7;
                break;
            default:
                numOfItems = 4;
                break;
        }
        //printList();
        SelectShoppingItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SelectShoppingItems()
    {
        //List<int> availableValues = new List<int>();

        itemsToSelect = numOfItems;
        int currItem;
        int item;
        //Picks items in shopping list
        for (int i = 0; i < numOfItems; i++)
        {
            //Select a random number within range
            currItem = UnityEngine.Random.Range(0, (totalNumofItems - i)); //INDEX LOCATION OF ITEM
            //Debug.Log("Index: " +  (currItem) + " selectableItems: " + (totalNumofItems - i));
            //Debug.Log(currItem);
            //itemsToSelect--;
            //Move item to back of list and shift items forward
            item = selectableItems[currItem];
            //Debug.Log("Item Value: " + item);
            //Debug.Log("Removing " + selectableItems.IndexOf(currItem) + " or " + currItem);
            // selectableItems.Remove(selectableItems.IndexOf(currItem)); // Removes from current location
            selectableItems.Remove(item);
            //Debug.Log("Item REMOVED");
            //printList();
            //selectableItems.Insert(totalNumofItems, currItem); // Moves item to the m of the list            
            selectableItems.Add(item);
            //printList();

        }

        //printList();
        for (int i = 0; i < numOfItems; i++)
        {
            //Create UI Item
            //Debug.Log(selectableItems[totalNumofItems - 1 - i]);
            createUIShoppingItem(selectableItems[totalNumofItems - 1 - i]);
            purchaseListUI.Add(selectableItems[totalNumofItems - 1 - i]);
                        
        }
    }


    private int getLevelNumber()
    {
        // [TODO: If multiple levels are created, make identifier.]
        return 1;
    }

    private void createUIShoppingItem(int itemIdentifier)
    {
        GameObject UIObject = Instantiate(shoppingSlotPrefab, this.transform);
        TextMeshProUGUI shopItemName = UIObject.GetComponentInChildren<TextMeshProUGUI>();
        shopItemName.text = getItemName(itemIdentifier); //[TODO: Placeholder
    }


    private void printList()
    {
        Debug.Log("New List");
        String list = "";
        foreach(int number in selectableItems)
        {
            list += number + ", ";
        }
        Debug.Log(list);
        /*for (int i = 0; i < selectableItems.Count; i++)
        {
            Debug.Log(selectableItems.IndexOf(i));
        }*/
    }

    private string getItemName(int index)
    {
        switch(index)
        {
            case 1:
                return "Drone";
            case 2:
                return "Television Set";
            case 3:
                return "Headphones";
            case 4:
                return "Cologne";
            case 5:
                return "Blender";
            case 6:
                return "Handbag";
            case 7:
                return "Computer Monitor";
            case 8:
                return "Game System";
            case 9:
                return "Coke";
            case 10:
                return "Pepsi";
            default:
                return "Missing item declaration for " + index;
        }
    }

    public List<int> getPurchaseListUI()
    {
        return purchaseListUI;
    }

}
