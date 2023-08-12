using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public GameObject slot;

    // Start is called before the first frame update
    void Start()
    {
        Inventory.onChangeInventory += InventoryChanged;
        Debug.Log("InventoryUI Start");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InventoryChanged(Dictionary<InventoryItemData, InventoryItem> inventories)
    {
        Debug.Log("InventoryChanged");

        foreach (Transform child in this.transform)
        {
            Destroy(child.gameObject);
        }

        foreach(InventoryItem item in inventories.Values)
        {
            GameObject newSlot = Instantiate(slot, this.transform);
            InventoryUISlot UIslot = newSlot.GetComponent<InventoryUISlot>();
            UIslot.icon.sprite = item.data.icon;
            UIslot.text.text = item.data.displayName;
        }

    }

}
