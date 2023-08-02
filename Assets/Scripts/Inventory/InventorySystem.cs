using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{

    public Dictionary<InventoryItemData, InventoryItem> items;
    public List<InventoryItem> inventories;

    void Start()
    {
        items = new Dictionary<InventoryItemData, InventoryItem>();
        inventories = new List<InventoryItem>();
    }

    public void Add(InventoryItemData reference)
    {
        if (items.TryGetValue(reference, out InventoryItem value)) 
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(reference);
            inventories.Add(newItem);
            items.Add(reference, newItem);
        }
    }

    public void Remove(InventoryItemData reference)
    {
        if (items.TryGetValue(reference, out InventoryItem value))
        {
            value.RemoveFromStack();
            if (value.stackSize == 0)
            {
                inventories.Remove(value);
                items.Remove(reference);

            }
        }
    }

    public InventoryItem Get(InventoryItemData reference)
    {
        if (items.TryGetValue(reference, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

}
