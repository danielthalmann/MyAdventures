using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public GameObject player;

    private AgentMoveTo agent;

    private GameObject currentObject;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Game Manager in the scene.");
        }

        instance = this;
    }

    private void Start()
    {
        currentObject = null;

        DialogManager.onDialogEventReference += OnDialogEventReference;
        DialogManager.onDialogStart += OnDialogStart;
        DialogManager.onDialogEnd += OnDialogEnd;
        InventoriesManager.onSelected += OnSelectedInventory;
        agent = player.GetComponent<AgentMoveTo>();
        agent.onAgentStop += OnAgentStop;


    }

    private void OnSelectedInventory()
    {
        InventoryItemData data = InventoriesManager.instance.GetCurrentInventoryItemData();

        CursorManager.instance.AttachImage(data.icon);

        InventoriesManager.instance.CloseInventoriesBox();
    }


    private void OnAgentStop()
    {
        Debug.Log("AgentStop");
        currentObject = agent.hitObject;

        if(currentObject != null)
        {
            DialogTrigger trigger = currentObject.GetComponent<DialogTrigger>();
            if (trigger != null)
            {
                trigger.TriggerDialogue();
            }
        }
    }

    private void OnDialogStart(GameObject gameObject)
    {
        Debug.Log("DialogStart");
        InputManager.instance.playerMove = false;
        currentObject = gameObject;
    }

    private void OnDialogEnd(GameObject gameObject)
    {
        InputManager.instance.playerMove = true;
    }

    private void OnDialogEventReference(string eventReference)
    {
        if (eventReference == "pick")
        {
            Debug.Log(eventReference);
            if (currentObject != null)
            {

                InventoryItemObject iventory = currentObject.GetComponent<InventoryItemObject>();
                Debug.Log(iventory);
                if (iventory != null)
                {
                    InventoriesManager.instance.Add(iventory.Pick());
                }

            }
        }
    }

}
