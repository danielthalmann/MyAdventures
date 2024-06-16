using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public InventoryItemData Pick()
    {
        Destroy(gameObject);

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
