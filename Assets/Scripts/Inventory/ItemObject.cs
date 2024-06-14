using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public InventoryItemData Pick()
    {
        Debug.Log("pick and destroy");

        Destroy(gameObject);
        Debug.Log("after destroy");

        return referenceItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            other.GetComponent<PickObject>().setItem(this.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PickObject>().setItem(null);
        }

    }
}
