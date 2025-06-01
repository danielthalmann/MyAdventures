using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;

    public InventoryItemData Pick()
    {
        Destroy(gameObject);

        return referenceItem;
    }

}
