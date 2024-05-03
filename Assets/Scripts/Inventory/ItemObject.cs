using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObject : MonoBehaviour
{
    public InventoryItemData referenceItem;
    public GameObject info;

    private Inventory inventory;
    private GameObject instance = null;

    private void Start()
    {
        // recherche dans toute la scène l'objet d'inventaire
        inventory = FindAnyObjectByType<Inventory>();
        if (inventory == null)
            Debug.Log("missing inventory");
              
    }

    private void Update()
    {
        if (instance != null)
        {
            Vector3 vscreen = Camera.main.WorldToScreenPoint(transform.position);
            instance.transform.position = vscreen;
        }
    }

    public InventoryItemData Pick()
    {

        Destroy(instance);
        Destroy(gameObject);
        instance = null;

        return referenceItem;
    }

    private void OnTriggerEnter(Collider other)
    {
        if ( other.tag == "Player")
        {
            Canvas canvas = FindAnyObjectByType<Canvas>();

            instance = Instantiate(info, canvas.transform);
            Debug.Log("trigger : " + other.tag);

            other.GetComponent<PickObject>().setItem(this.gameObject);
        }
        
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            Destroy(instance);
            instance = null;
            Debug.Log("trigger : " + other.tag);

            other.GetComponent<PickObject>().setItem(null);
        }


    }
}
