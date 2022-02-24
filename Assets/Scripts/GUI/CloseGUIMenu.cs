using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseGUIMenu : MonoBehaviour
{
    public GameObject objToClose;

    public void Close(bool open)
    {
        objToClose.SetActive(open);
    }
}
