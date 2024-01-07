using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class PurchaseElementPanel : MonoBehaviour
{
    public TMP_Text itemNameText;
    public TMP_Text countText;

    public PurchaseElement element;
    public void SetPurchaseElement(PurchaseElement e)
    {
        element = e;

        //item name pop up
        itemNameText.text = e.itemName;
        countText.text = e.count.ToString();
    }
}
