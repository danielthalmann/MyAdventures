using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInventor : MonoBehaviour
{
    public Inventories inventories;
    public ItemInventory item;


    // Start is called before the first frame update
    void Start()
    {

        inventories.addItem(item);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
