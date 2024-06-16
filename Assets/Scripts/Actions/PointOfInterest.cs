using FMOD;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;


public class PointOfInterest : MonoBehaviour
{
    public string interestName = "";

    public bool canBeWatched = false;
    public bool canBeTaked = false;
    public bool canBeCombined = false;

    [Header("Dialogs")]
    public Dialogue watchDialogue;
    public Dialogue TakeDialogue;


    [Header("Item Data")]
    public InventoryItemData referenceInventoryItem;

    [Header("Event")]
    public UnityEvent onCombine;
    public UnityEvent onPickItem;


    // Start is called before the first frame update
    void Start()
    {
        if (this.GetComponent<Collider>() == null)
        {
            throw new System.Exception("Missing collider");
        }
        if (canBeTaked && referenceInventoryItem == null)
        {
           throw new System.Exception("Missing item object component");
        }
        if (canBeWatched && watchDialogue == null)
        {
            throw new System.Exception("Missing watch dialog");
        }
        /*
        if (canBeTaked && watchDialogue == null)
        {
            throw new System.Exception("Missing watch dialog");
        }
        */
    }

    private void OnMouseOver()
    {
        PointOfInterestManager.instance.setSelection(this.gameObject);
    }

    private void OnMouseExit()
    {
        PointOfInterestManager.instance.setSelection(null);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            PointOfInterestManager.instance.setPointOfInterest(this.gameObject);
        }

    }

    private void OnMouseUp()
    {
        PointOfInterestManager.instance.GoToSelection();
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            PointOfInterestManager.instance.setPointOfInterest(null);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
