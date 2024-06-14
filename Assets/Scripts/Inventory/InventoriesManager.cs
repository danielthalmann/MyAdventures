using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoriesManager : MonoBehaviour
{

    [SerializeField]
    public Dictionary<InventoryItemData, InventoryItem> inventories;

    public delegate void OnChangeInventory(Dictionary<InventoryItemData, InventoryItem> inventories);
    public static OnChangeInventory onChangeInventory;

    public static InventoriesManager instance { get; private set; }


    public GameObject uiIventoriesBox;
    public GameObject uiIventoriesList;
    public GameObject uiSlot;

    public bool inventoriesOpen = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Audio Manager in the scene.");
        }

        instance = this;
        inventories = new Dictionary<InventoryItemData, InventoryItem>();

    }

    private void Start()
    {
        uiIventoriesBox.SetActive(inventoriesOpen);
        UpdateUI();
    }


    /// <summary>
    /// fermeture de l'inventaire
    /// </summary>
    public void CloseInventoriesBox()
    {
        inventoriesOpen = false;
        uiIventoriesBox.SetActive(inventoriesOpen);
    }

    /// <summary>
    /// alterne l'ouverture et la fermeture de l'inventaire
    /// </summary>
    public void ToggleInventoriesBox()
    {
        inventoriesOpen= !inventoriesOpen;
        uiIventoriesBox.SetActive(inventoriesOpen);
    }

    /// <summary>
    /// Ajoute une entrée dans l'inventaire
    /// </summary>
    /// <param name="reference"></param>
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

        ChangeInventory();
    }


    /// <summary>
    /// Supprime de l'inventaire un élément
    /// </summary>
    /// <param name="reference"></param>
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

    /// <summary>
    /// Obtient l'élément de l'inventaire 
    /// </summary>
    /// <param name="reference"></param>
    /// <returns></returns>
    public InventoryItem Get(InventoryItemData reference)
    {
        if (inventories.TryGetValue(reference, out InventoryItem value))
        {
            return value;
        }
        return null;
    }


    /// <summary>
    /// emet l'événement de changement de l'inventaire
    /// </summary>
    public void ChangeInventory()
    {
        onChangeInventory?.Invoke(inventories);

        UpdateUI();
    }

    /// <summary>
    /// met à jour l'ui
    /// </summary>
    public void UpdateUI()
    {

        foreach (Transform child in uiIventoriesList.transform)
        {
            Destroy(child.gameObject);
        }

        foreach (InventoryItem item in inventories.Values)
        {
            GameObject newSlot = Instantiate(uiSlot, uiIventoriesList.transform);
            InventoryUISlot UIslot = newSlot.GetComponent<InventoryUISlot>();
            UIslot.icon.sprite = item.data.icon;
            UIslot.text.text = item.data.displayName;
            UIslot.number.text = item.stackSize.ToString();
        }

    }


}
