using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointOfInterestManager : MonoBehaviour
{
    [Header("Player")]
    public GameObject Player = null;

    [Header("UI Point of Actions")]
    public GameObject UiActionBox = null;
    public TMP_Text UiActionTitle = null;
    public GameObject UiPick = null;
    public GameObject UiWatch = null;

    [Header("UI Select Box")]
    public GameObject UiSelectBox = null;
    public TMP_Text UiSelectTitle = null;

    public static PointOfInterestManager instance;

    [Header("Internal")]
    public GameObject pointOfInterest;
    public GameObject selection;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Point of Interest Manager in the scene.");
        }

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        if (UiSelectBox != null)
            UiSelectBox.SetActive(false);
        if (UiActionBox != null)
            UiActionBox.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setSelection(GameObject selection)
    {
        this.selection = selection;
        UpdateUiSelection();
    }

    public void GoToSelection()
    {
        if (selection != null && Player != null)
        {
            Player.GetComponent<AgentMoveTo>().SetDestination(selection.transform.position);
        }
    }

    public void WatchItem()
    {
        if (pointOfInterest != null)
        {
            PointOfInterest interest = pointOfInterest.GetComponent<PointOfInterest>();

            if (interest.watchDialogue != null)
            {
                DialogManager.instance.StartDialogue(interest.watchDialogue);
            }

        }
    }

    public void PickItem()
    {
        if (pointOfInterest != null)
        {
            PointOfInterest interest = pointOfInterest.GetComponent<PointOfInterest>();
            
            if(interest.referenceInventoryItem != null)
            {
                InventoriesManager.instance.Add(interest.referenceInventoryItem);
            }

            if (interest.TakeDialogue != null)
            {
                DialogManager.instance.StartDialogue(interest.TakeDialogue);
            }

            Destroy(pointOfInterest);
            setPointOfInterest(null);
        }
    }

    public void setPointOfInterest(GameObject pointOfInterest)
    {
        this.pointOfInterest = pointOfInterest;
        UpdateUiActions();

    }

    private void UpdateUiSelection()
    {

        if (UiSelectBox == null)
            return;

        if (selection != null && pointOfInterest != selection)
        {
            Vector3 vscreen = Camera.main.WorldToScreenPoint(selection.transform.position);
            UiSelectBox.transform.position = vscreen + new Vector3(0,20.0f,0);

            PointOfInterest poi = selection.GetComponent<PointOfInterest>();

            UiSelectBox.SetActive(true);
            UiSelectTitle.text = poi.referenceInventoryItem.name;

        }
        else
        {
            UiSelectBox.SetActive(false);
        }

    }

    private void UpdateUiActions()
    {

        if (UiActionBox == null)
            return;

        if (pointOfInterest != null)
        {
            Vector3 vscreen = Camera.main.WorldToScreenPoint(pointOfInterest.transform.position);
            UiActionBox.transform.position = vscreen + new Vector3(0, 20.0f, 0);

            PointOfInterest poi = pointOfInterest.GetComponent<PointOfInterest>();

            UiActionTitle.text = poi.referenceInventoryItem.name;

            UiActionBox.SetActive(true);
            UiWatch.SetActive(poi.canBeWatched);
            UiPick.SetActive(poi.canBeTaked);

        }
        else
        {
            UiActionBox.SetActive(false);
            UiWatch.SetActive(false);
            UiPick.SetActive(false);
        }

    }

}
