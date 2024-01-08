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

            if (player.takeItem == null)
                return;
          
            //리스트 일괄 담기
            addedItems.Add(player.takeItem);

            player.takeItem.transform.parent = null;

            player.takeItem.transform.position = itemPlace.position;
            player.takeItem.GetComponent<Rigidbody>().isKinematic = false;

            Collider col = player.takeItem.GetComponentInChildren<Collider>();
            col.enabled = true;

            player.takeItem = null;
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