using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class PickObject : MonoBehaviour
{
    private GameObject item = null;

    public GameObject UiPickInfoBox = null;
    public TMP_Text UiTitle = null;
    public TMP_Text UiDescription = null;

    public UnityEvent onPickItem;


    private void Start()
    {
        if (UiPickInfoBox != null)
            UiPickInfoBox.SetActive(false);
    }

    public void setItem(GameObject item)
    {
        this.item = item;
        UpdateUi();

    }

    public void PickItem()
    {

        if (item != null)
        {
            InventoriesManager.instance.Add(item.GetComponent<InventoryItemObject>().Pick());
            onPickItem.Invoke();
            item = null;
            UpdateUi();
        }

    }

    private void UpdateUi()
    {
        if (UiPickInfoBox == null)
            return;

        if (item != null)
        {
            InventoryItemObject itemObject = item.GetComponent<InventoryItemObject>();
            UiPickInfoBox.SetActive(true);
            UiTitle.text = itemObject.referenceItem.name;
            UiDescription.text = itemObject.referenceItem.description;
        }
        else
        {
            UiPickInfoBox.SetActive(false);
        }

    }

}
