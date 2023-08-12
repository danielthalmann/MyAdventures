using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class PickObject : MonoBehaviour
{
    private Inventory inventory;
    private GameObject item = null;

    private void Start()
    {
        // recherche dans toute la scène l'objet d'inventaire
        inventory = FindAnyObjectByType<Inventory>();
        if (inventory == null)
            Debug.Log("missing inventory");
              
    }


    public void setItem(GameObject item)
    {
        this.item = item;
    }


    public void OnPickItem()
    {
        Debug.Log("pick");

        if (item != null)
        {
            inventory.Add(item.GetComponent<ItemObject>().Pick());
        }

    }

}
