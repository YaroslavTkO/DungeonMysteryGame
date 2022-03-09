using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadmap : MonoBehaviour
{
  

    // Update is called once per frame
    public void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.gameObject.CompareTag("map"))
            SceneManager.LoadScene(0);
    }
}

