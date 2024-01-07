using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cart : MonoBehaviour
{
    public Transform itemPlace; //플레이가 획득 아이템들이 놓이는 장소


    public List<Item> addedItems = new List<Item>();//카트에 추가된 아이템
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            for (int i = 0; i < addedItems.Count; i++)
            {
                Debug.Log(addedItems[i].itemName);
            }
            //리스트 일괄 담기
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
        //addedItems와 어떤 값과 비교해야될까요?

        //플레이어의 구매 목록 가져오기
        List<PurchaseElement> purchaseList = Player.Instance.purchaseList;

        PurchaseListPanel purchaseListPanel = FindObjectOfType<PurchaseListPanel>();
        Dictionary<string, int> itemCount = new Dictionary<string, int>();

        bool clear = true;
        for (int i = 0; i < purchaseList.Count; i++)
        {

            int addedCount = GetAddedItemCount(purchaseList[i].itemName);
            itemCount.Add(purchaseList[i].itemName, addedCount);
            if (addedCount < purchaseList[i].count)//만족하지 못한 개수가 있음
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