using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pss : MonoBehaviour
{
    public GameObject r;
    public GameObject r1;


    private void Start()
    {
        

       // an = GetComponent<Animator>();
    }
    public void OnTriggerEnter2D(Collider2D coll)
    
    {
        if (coll.tag == "Player")
        {
            r.SetActive(false);
            r1.SetActive(false);
        }
    }

}

  
