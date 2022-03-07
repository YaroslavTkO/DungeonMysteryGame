using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleActiveState : MonoBehaviour
{
    public GameObject objToClose;

    public void SetActive(bool open)
    {
        objToClose.SetActive(open);
    }
}
