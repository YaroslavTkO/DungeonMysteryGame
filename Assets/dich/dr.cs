using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dr : MonoBehaviour
{

    public Item item;

    public void ButtonClicked()
    {
        GetComponent<SpriteRenderer>().sprite = item.itemSprite;
    }
}

