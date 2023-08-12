using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    public Dictionary<InventoryItemData, InventoryItem> inventories;

    public delegate void OnChangeInventory(Dictionary<InventoryItemData, InventoryItem> inventories);
    public static OnChangeInventory onChangeInventory;


    private void Awake()
    {
        inventories = new Dictionary<InventoryItemData, InventoryItem>();
    }

    public void Add(InventoryItemData reference)
    {
        if (inventories.TryGetValue(reference, out InventoryItem value)) 
        {
            value.AddToStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(reference);
            inventories.Add(reference, newItem);
        }

        onChangeInventory?.Invoke(inventories);

        Debug.Log("items : " + inventories.Count);
    }

    public void Remove(InventoryItemData reference)
    {
        if (inventories.TryGetValue(reference, out InventoryItem value))
        {
            value.RemoveFromStack();
            if (value.stackSize == 0)
            {
                inventories.Remove(reference);
            }
        }
    }

    public InventoryItem Get(InventoryItemData reference)
    {
        if (inventories.TryGetValue(reference, out InventoryItem value))
        {
            return value;
        }
        return null;
    }

}
