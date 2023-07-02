using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item Data", menuName = "Scriptable Objets/Inventory Item Data", order = 0)]
public class InventoryItemData : ScriptableObject
{
    public string id;
    public string displayName;
    public Sprite icon;
    public GameObject prefab;

}
