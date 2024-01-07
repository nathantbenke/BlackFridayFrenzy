using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Market : MonoBehaviour
{
    public static Market Instance;

    public Stall[] stalls;
    public Transform exitTr; // exit
    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        stalls = FindObjectsOfType<Stall>();
    }
    // Contains all items existing in the market
    [SerializeField] List<Item> items = new List<Item>();

    //Return stalls holding objects with itemName
    public List<Stall> GetStalls(string itemName)
    {
        List<Stall> list = new List<Stall>();
        for (int i = 0; i < stalls.Length; i++)
        {
            for (int j = 0; j < stalls[i].itemPrefabs.Length; j++)
            {
                if (stalls[i].itemPrefabs[j].itemName == itemName)
                {
                    list.Add(stalls[i]);
                    break;
                }
            }
        }
        return list;
    }
    public void AddItem(Item i)
    { 
        items.Add(i); 
    }
}
