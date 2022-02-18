using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GroundItem : MonoBehaviour
{
    public ItemObject item;
    public void Start()
    {
        GetComponent<SpriteRenderer>().sprite = item.uiDisplay;
    }
}
