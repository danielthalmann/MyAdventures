using NUnit.Framework.Interfaces;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUISlot : MonoBehaviour
{
    public Image icon;
    public TMP_Text text;
    public TMP_Text number;
    [Header("data inventory")]
    public InventoryItemData data;

    public void SelectItem()
    {
        if (data != null)
        {
            InventoriesManager.instance.Select(data);
        }
    }

}
