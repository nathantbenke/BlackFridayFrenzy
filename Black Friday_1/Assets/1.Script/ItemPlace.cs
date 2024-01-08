using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPlace : MonoBehaviour
{
    public Item[] itemPrefabs;
    public Item curItem;

    private void Start()
    {
        SpawnItem();
    }

    void SpawnItem()
    {
        Item prefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];
        curItem = Instantiate(prefab, transform);

        curItem.transform.position = transform.position;
    }
    public Item TakeItem()
    {
        if (curItem == null)
            return null;

        Item item = curItem;

        //curItem.gameObject.SetActive(false);
        curItem = null;

        //Call the SpawnItem() function after 5 seconds
        Invoke("SpawnItem", 5);


        return item;
    }
}
