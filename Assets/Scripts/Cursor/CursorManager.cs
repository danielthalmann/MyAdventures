using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CursorManager : MonoBehaviour
{

    [SerializeField]
    public CursorInfo[] cursors;
    public int defaultCursorIndex = 0;
    public bool visible = false;

    public Image handleCursor;
    public Image attachedToCursor;

    public static CursorManager instance;

    private CursorInfo currentCursor;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Cursor Manager in the scene.");
        }

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        Canvas canvas = FindAnyObjectByType<Canvas>();
        if (!canvas)
        {
            Debug.LogError("Fail load cursor. The scene can contain canvas");
        }

        Cursor.visible = visible;
        if (handleCursor == null)
        {
            GameObject gameObject = new GameObject();

            handleCursor = gameObject.AddComponent<Image>();
            gameObject.name = "Cursor";
            gameObject.transform.parent = canvas.transform;
            handleCursor.SetNativeSize();
        }

        handleCursor.raycastTarget = false;

        if (attachedToCursor == null)
        {
            GameObject gameObject = new GameObject();

            attachedToCursor = gameObject.AddComponent<Image>();
            gameObject.name = "Cursor";
            gameObject.transform.parent = canvas.transform;
        }

        attachedToCursor.raycastTarget = false;


        attachedToCursor.gameObject.SetActive(false);

        CursorIndex(defaultCursorIndex);
    }

    private void Update()
    {
        Vector2 cursorPos = (Input.mousePosition);
        handleCursor.gameObject.transform.position = currentCursor.hotspot + cursorPos;
    }

    public void AttachImage(Sprite sprite)
    {
        if (sprite != null)
        {
            attachedToCursor.sprite = sprite;
            attachedToCursor.gameObject.SetActive(true);
        } else
        {
            attachedToCursor.gameObject.SetActive(false);
        }
    }

    public void SetCursorInfo(CursorInfo cursor)
    {
        handleCursor.sprite = cursor.cursor;
        RectTransform rect = handleCursor.GetComponent<RectTransform>();
        rect.sizeDelta = cursor.size;
        currentCursor = cursor;
    }

    public void CursorTag(string tag)
    {
        for(int i = 0; i < cursors.Length; i++)
        {
            if (cursors[i].tag == tag)
            {
                CursorIndex(i);
                return;
            }
        }
    }


    public void CursorIndex(int cursorIndex)
    {
        if (cursorIndex < cursors.Length)
        {
            SetCursorInfo(cursors[cursorIndex]);
        }
    }

}
