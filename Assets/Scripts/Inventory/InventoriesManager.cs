using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoriesManager : MonoBehaviour
{

    [SerializeField]
    public Dictionary<InventoryItemData, InventoryItem> inventories;

    public delegate void OnAddItem(InventoryItemData itemData);
    public static OnAddItem onAddItem;

    public delegate void OnRemoveItem(InventoryItemData itemData);
    public static OnRemoveItem onRemoveItem;

    public delegate void OnSelected();
    public static OnSelected onSelected;


    public static InventoriesManager instance { get; private set; }

    public GameObject uiIventoriesBox;
    public GameObject uiIventoriesList;
    public GameObject uiSlot;

    public bool inventoriesOpen = false;

    private InventoryItemData currentSelected;

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
        StartCoroutine(InventoriesBox());
    }

    /// <summary>
    /// alterne l'ouverture et la fermeture de l'inventaire
    /// </summary>
    public void ToggleInventoriesBox()
    {
        inventoriesOpen= !inventoriesOpen;
        StartCoroutine(InventoriesBox());
    }

    IEnumerator InventoriesBox()
    {
        yield return new WaitForSeconds(.01f);
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

        onAddItem?.Invoke(reference);
        UpdateUI();
        
    }


    /// <summary>
    /// Supprime de l'inventaire un élément
    /// </summary>
    /// <param name="reference"></param>
    public void Remove(InventoryItemData reference)
    {
        Debug.Log(reference);
        if (inventories.TryGetValue(reference, out InventoryItem value))
        {
            Debug.Log(value.stackSize);
            value.RemoveFromStack();
            if (value.stackSize == 0)
            {
                Debug.Log(value.stackSize);
                inventories.Remove(reference);
            }
        }

        if (currentSelected == reference)
        {
            currentSelected = null;
        }

        onRemoveItem?.Invoke(reference);
        UpdateUI();

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
    /// Selectionne l'élément de l'inventaire 
    /// </summary>
    /// <param name="reference"></param>
    public void Select(InventoryItemData reference)
    {
        if(reference == null)
        {
            currentSelected = null;
        }
        else
        {
            if (inventories.TryGetValue(reference, out InventoryItem value))
            {
                currentSelected = reference;
            } else
            {
                currentSelected = null;
            }
        }

        onSelected!.Invoke();

    }

    public void ClearSelection()
    {
        Select(null);
    }

    public InventoryItemData GetCurrentInventoryItemData()
    {
        return currentSelected;
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

        foreach (InventoryItemData itemData in inventories.Keys)
        {

            InventoryItem item = inventories[itemData];
            GameObject newSlot = Instantiate(uiSlot, uiIventoriesList.transform);
            InventoryUISlot UIslot = newSlot.GetComponent<InventoryUISlot>();
            UIslot.icon.sprite = item.data.icon;
            UIslot.text.text = item.data.displayName;
            UIslot.data = itemData;

        }

    }


}
