using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerSetActive : MonoBehaviour
{
    public GameObject toActive;

    private void Start()
    {
        toActive.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.CompareTag("Player"))
            toActive.SetActive(true);
    }
}
