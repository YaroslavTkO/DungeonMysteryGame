using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generatemap : MonoBehaviour
{
    public Transform tree;
    public int treecount;
    private Vector2 mapSize= new Vector2(50,50);
    // Start is called before the first frame update
    void Start()
    {
        generateMap();
    }
    void generateMap()
    {
        treecount = 30;
        for(int i = 0; i < treecount; i++)
        {
            Instantiate(tree,new Vector2 (Random.Range(0,mapSize.x), Random.Range(0, mapSize.x)),tree.rotation);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
