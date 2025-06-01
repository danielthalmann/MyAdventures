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

    private Image handleCursor;

    public static CursorManager instance;

    private Vector2 cursorOffset;

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
        Cursor.visible = visible;
        ChangeCursor(defaultCursorIndex);
        GameObject gameObject = new GameObject();
        handleCursor = gameObject.AddComponent<Image>();
    }

    private void Update()
    {
        Vector2 cursorPos = (Input.mousePosition);
        handleCursor.gameObject.transform.position = cursorOffset + cursorPos;
    }

    public void ChangeCursor(int cursorIndex)
    {
        if (cursorIndex < cursors.Length)
        {
            handleCursor.sprite = cursors[cursorIndex].cursor;
            cursorOffset = cursors[cursorIndex].offset;
        }
    }

}
