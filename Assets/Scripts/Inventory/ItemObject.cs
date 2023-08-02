using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    private InventorySystem inventory;

    private void Start()
    {
        // recherche dans toute la scène l'objet d'inventaire
        inventory = FindAnyObjectByType<InventorySystem>();
        if (inventory == null)
            Debug.Log("missing inventory");
              

    }

    public void OnPickItem()
    {
        inventory.Add(referenceItem);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("trigger : " + other.tag);
    }

    private void OnTriggerExit(Collider other)
    {
        
    }
}
