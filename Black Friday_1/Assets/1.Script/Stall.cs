using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stall : MonoBehaviour
{
    public ItemPlace[] itemPlaces;
    public Item[] itemPrefabs;
    private void Awake()
    {
        itemPlaces = GetComponentsInChildren<ItemPlace>();

        for (int i = 0; i < itemPlaces.Length; i++)
        {
            itemPlaces[i].itemPrefabs = itemPrefabs;
        }
    }
}
