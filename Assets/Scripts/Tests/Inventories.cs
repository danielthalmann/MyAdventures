using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Inventories", menuName = "Scriptable Objets/Inventories", order = 0)]
public class Inventories : ScriptableObject
{ 

    public List<ItemInventory> items = new();

    public void addItem(ItemInventory item)
    {
        items.Add(item);
        
    }


}
