using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sprite : MonoBehaviour
{
    public float offset = 0;
    private int sortingOrderBase = 0;
    private Renderer renderer;

    // Start is called before the first frame update
    private void Awake()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        renderer.sortingOrder = (int)(sortingOrderBase - transform.position.y + offset);
    }
}
