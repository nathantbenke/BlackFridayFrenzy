using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public string itemName; 

    bool sizeUp;
    float size;

    public void OnEnable()
    {
        SizeUp();
    }

    public void SizeUp()
    {
        sizeUp = true;
        size = 0;
        transform.localScale = new Vector3(size, size, size);
    }

    private void Update()
    {
        if (!sizeUp)
            return;

        if (size >= 1)
        {
            sizeUp = false;
            transform.localScale = new Vector3(1, 1, 1);
            return;
        }

        size += Time.deltaTime * 5;
        transform.localScale = new Vector3(size, size, size);
    }


}