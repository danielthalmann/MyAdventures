using System.Collections;
using UnityEngine;

public class ItemCombine : MonoBehaviour
{

    [Header("Dialog")]
    public Dialogue dialogFalse;
    public Dialogue dialogTrue;

    [Header("Test item id")]
    public string combineId;

    [Header("Destroy if")]
    public bool destroyItemOnTrue = true;
    public bool destroyItemOnFalse = false;


    public bool CanCombine(InventoryItemData data)
    {
        return (data.id == combineId);
    }

    public void TriggerDialogueTrue(InventoryItemData data)
    {
        DialogManager.instance.StartObjectDialog(this.gameObject, dialogTrue);

        if (destroyItemOnTrue)
            InventoriesManager.instance.Remove(data);
    }

    public void TriggerDialogueFalse(InventoryItemData data)
    {
        DialogManager.instance.StartObjectDialog(this.gameObject, dialogFalse);

        if (destroyItemOnFalse)
            InventoriesManager.instance.Remove(data);
    }

}
