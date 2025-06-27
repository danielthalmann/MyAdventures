using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static DialogManager;

public class InputManager : MonoBehaviour
{
    public AgentMoveTo agent;
    public Camera cam;
    public LayerMask mask;
    public bool allowMove = true;
    public PauseMenu pauseMenu;

    private bool dialogOpen = false;
    public static InputManager instance { get; private set; }

    // Use this for initialization
    void Start()
    {
        if (cam == null)
        {
            cam = Camera.main;
        }
        DialogManager.onDialogStart += OnDialogStart;
        DialogManager.onDialogEnd += OnDialogEnd;

        pauseMenu.onPause += OnPause;
        pauseMenu.onResume += OnResume;

    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Input Manager in the scene.");
        }

        instance = this;
    }

    private void OnPause()
    {
        allowMove = false;
    }
    private void OnResume()
    {
        allowMove = true;
    }

    private void OnDialogStart(GameObject gameObject)
    {
        dialogOpen = true;
    }

    private void OnDialogEnd(GameObject gameObject)
    {
        dialogOpen = false;
    }

    public bool PointerIsOverUI(Vector2 screenPos)
    {
        var hitObject = UIRaycast(ScreenPosToPointerData(screenPos));
        return hitObject != null && hitObject.layer == LayerMask.NameToLayer("UI");
    }

    GameObject UIRaycast(PointerEventData pointerData)
    {
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        return results.Count < 1 ? null : results[0].gameObject;
    }

     PointerEventData ScreenPosToPointerData(Vector2 screenPos)
       => new(EventSystem.current) { position = screenPos };

    // Update is called once per frame
    void Update()
    {
        if (dialogOpen)
        {
            if(Input.GetMouseButtonUp(0))
            {
                DialogManager.instance.DisplayNextSentence();
            }
        } 
        else
        {
            if (allowMove && Input.GetKeyUp(KeyCode.Escape))
            {
                if (pauseMenu != null)
                {
                    pauseMenu.Pause();
                }
            }

            if (allowMove && Input.GetMouseButtonUp(0))
            {
                if (!PointerIsOverUI(Input.mousePosition))
                {
                    Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit, 100, mask)) { 
                
                        GameObject hitObject = hit.collider.gameObject;
                        PointOfInterest poi = hitObject.GetComponent<PointOfInterest>();
                        if (null != poi)
                        {
                            agent.SetDestination(poi.GetPointOfInterestDestination(), hitObject);
                        }
                        else
                        {
                            agent.SetDestination(hit.point, null);
                        }
                    }

                }

            }

        }
        
    }
}
