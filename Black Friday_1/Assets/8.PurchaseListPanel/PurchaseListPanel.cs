using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PurchaseListPanel : MonoBehaviour
{
    public PurchaseElementPanel[] elementPanels;
    public void SetPurchaseList(List<PurchaseElement> elements)
    {
        for (int i = 0; i < elementPanels.Length; i++)
        {
            if (i < elements.Count)
            {
                elementPanels[i].gameObject.SetActive(true);
                elementPanels[i].SetPurchaseElement(elements[i]);
            }
            else
            {
                elementPanels[i].gameObject.SetActive(false);
            }
        }
    }

    public void CheckCount(Dictionary<string, int> itemCount)
    {
        for (int i = 0; i < elementPanels.Length; i++)
        {
            if (!elementPanels[i].gameObject.activeSelf)
                continue;

            foreach (var data in itemCount)
            {
                if (data.Key == elementPanels[i].element.itemName)
                {
                    if (data.Value >= elementPanels[i].element.count)
                    {
                        elementPanels[i].countText.color = Color.green;
                    }
                    else
                    {
                        elementPanels[i].countText.color = Color.black;
                    }
                    break;
                }
            }
        }
    }
}