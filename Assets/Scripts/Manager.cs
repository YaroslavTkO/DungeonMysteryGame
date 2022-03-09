using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    public GameObject player;
    public Transform spawnPos;

    private void Start()
    {
        if (PlayerPrefs.GetString("lastLevel").Equals("level1"))
        {
            player.transform.position = spawnPos.position;
            PlayerPrefs.DeleteKey("lastLevel");
        }
    }
}
