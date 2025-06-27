using UnityEditor.Overlays;
using UnityEngine;
using static SceneLoaderManager;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public GameObject player;

    private AgentMoveTo agent;

    private GameObject selectedObject;

    private bool loadingSavedGame = false;
    private SaveData savedData;

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
        selectedObject = null;

        DialogManager.onDialogEventReference += OnDialogEventReference;
        DialogManager.onDialogStart += OnDialogStart;
        DialogManager.onDialogEnd += OnDialogEnd;
        InventoriesManager.onSelected += OnSelectedInventory;
        InventoriesManager.onRemoveItem += OnRemoveItem;
        SceneLoaderManager.onStartLoading += OnStartLoading;
        SceneLoaderManager.onFinishLoading += OnFinishLoading;
        SceneLoaderManager.onCompletedLoading += OnCompletedLoading;
        agent = player.GetComponent<AgentMoveTo>();
        agent.onAgentStop += OnAgentStop;

    }

    private void OnSelectedInventory()
    {
        InventoryItemData data = InventoriesManager.instance.GetCurrentInventoryItemData();

        if (data)
        {
            CursorManager.instance.AttachImage(data.icon);
        } else
        {
            CursorManager.instance.AttachImage(null);
        }

        InventoriesManager.instance.CloseInventoriesBox();

    }

    private void OnStartLoading()
    {
        InputManager.instance.allowMove = false;
    }

    private void OnFinishLoading()
    {
        InputManager.instance.allowMove = true;
    }

    private void OnCompletedLoading()
    {
        LoadSavedData();
    }

    private void OnAgentStop()
    {
        selectedObject = agent.hitObject;

        if(selectedObject != null)
        {
            InventoryItemData data = InventoriesManager.instance.GetCurrentInventoryItemData();
            if (data == null)
            {
                DialogTrigger trigger = selectedObject.GetComponent<DialogTrigger>();
                if (trigger != null)
                {
                    trigger.TriggerDialogue();
                }
            } else
            {
                ItemCombine combine = selectedObject.GetComponent<ItemCombine>();

                if (combine != null)
                {
                    if (combine.CanCombine(data))
                        combine.TriggerDialogueTrue(data);
                    else
                        combine.TriggerDialogueFalse(data);
                }

            }

        }
    }

    private void OnDialogStart(GameObject gameObject)
    {
        InputManager.instance.allowMove = false;
        selectedObject = gameObject;
    }

    private void OnDialogEnd(GameObject gameObject)
    {
        InputManager.instance.allowMove = true;
    }

    private void OnDialogEventReference(string eventReference)
    {
        if (eventReference == "pick")
        {
            if (selectedObject != null)
            {
                InventoryItemObject iventory = selectedObject.GetComponent<InventoryItemObject>();

                if (iventory != null)
                {
                    InventoriesManager.instance.Add(iventory.Pick());
                }

            }
        }
    }

    private void OnRemoveItem(InventoryItemData item)
    {
        if(InventoriesManager.instance.GetCurrentInventoryItemData() == null)
        {
            CursorManager.instance.AttachImage(null);
        }
    }

    public void Save(ref SaveData data)
    {
        data.SceneName = SceneLoaderManager.instance.GetCurrentSceneName();
        data.PlayerPosition = agent.transform.position;
    }

    public void Load(SaveData data)
    {
        SceneLoaderManager.instance.LoadScene(data.SceneName);
        loadingSavedGame = true;
        savedData = data;
        //LoadSavedData();
    }

    private void LoadSavedData()
    {
        agent.transform.position = savedData.PlayerPosition;
        loadingSavedGame = false;
    }

}
