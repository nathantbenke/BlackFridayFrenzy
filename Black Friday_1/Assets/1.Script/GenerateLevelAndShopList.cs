using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateLevelAndShopList : MonoBehaviour
{

    public GameObject item1;
    public GameObject item2;
    public GameObject item3;
    public GameObject item4;
    public GameObject item5;
    public GameObject item6;
    public GameObject item7;

    public GameObject shoppingSlotContainer;

    private List<int> selectableItems = new List<int> { 1, 2, 3, 4, 5, 6, 7 };
    private int totalNumofItems = 7;

    private int levelNum;
    private int numOfItems;
    private int itemsToSelect;
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
        generateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void generateItems()
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
            Debug.Log(selectableItems[totalNumofItems - 1 - i]);

                        
        }
    }


    private int getLevelNumber()
    {
        return 1;
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

}
