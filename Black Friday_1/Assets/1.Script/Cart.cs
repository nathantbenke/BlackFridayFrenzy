using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public Transform itemPlace; 


    public List<Item> addedItems = new List<Item>();//items which add in the cart
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            for (int i = 0; i < addedItems.Count; i++)
            {
                Debug.Log(addedItems[i].itemName);
            }
            //add all lists
            addedItems.AddRange(player.takenItems);
            for (int i = 0; i < player.takenItems.Count; i++)
            {
                player.takenItems[i].transform.position = itemPlace.position;
                player.takenItems[i].GetComponent<Rigidbody>().isKinematic = false;
                player.takenItems[i].gameObject.SetActive(true);
            }

            player.takenItems.Clear();
            CheckClear();
        }
    }

    public void CheckClear()
    {

        //Bring the Player's purchaseList
        List<PurchaseElement> purchaseList = Player.Instance.purchaseList;

        PurchaseListPanel purchaseListPanel = FindObjectOfType<PurchaseListPanel>();
        Dictionary<string, int> itemCount = new Dictionary<string, int>();

        bool clear = true;
        for (int i = 0; i < purchaseList.Count; i++)
        {

            int addedCount = GetAddedItemCount(purchaseList[i].itemName);
            itemCount.Add(purchaseList[i].itemName, addedCount);
            if (addedCount < purchaseList[i].count)
            {
                clear = false;
            }
        }

        purchaseListPanel.CheckCount(itemCount);
        if (!clear)
        {
            return;
        }
        GameManager.Instance.EndGame(true);
    }

    public int GetAddedItemCount(string itemName)
    {
        int count = 0;
        for (int i = 0; i < addedItems.Count; i++)
        {
            if (addedItems[i].itemName.Equals(itemName))
            {
                count++;
            }
        }
        return count;
    }
}