using UnityEngine;
using UnityEngine.Device;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using System.Collections;

public class PointOfInterestUI : MonoBehaviour
{
    private UIDocument uiDocument;
    private Label label;

    private Vector3 position;
    private bool isShow = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        uiDocument = GetComponent<UIDocument>();
        label = uiDocument.rootVisualElement.Q<UnityEngine.UIElements.Label>("Title");
    }

    /*
    private void FixedUpdate()
    {
        uiDocument.rootVisualElement.style.top = position.y;
        uiDocument.rootVisualElement.style.left = position.x;

        label.text = text;
    }
    */

    public bool IsShow()
    {
        return (uiDocument.rootVisualElement.style.display == DisplayStyle.Flex);
    }

    public void Hide()
    {
        uiDocument.rootVisualElement.style.display = DisplayStyle.None;
    }

    public void Show()
    {
        uiDocument.rootVisualElement.style.visibility = Visibility.Hidden;
        uiDocument.rootVisualElement.style.display = DisplayStyle.Flex;
        StartCoroutine(ShowEnumerator());

    }
    
    IEnumerator ShowEnumerator()
    {
        yield return new WaitForSeconds(.1f);
        uiDocument.rootVisualElement.style.visibility = Visibility.Visible;
    }


    public void SetPosition(Vector3 position)
    {
        this.position = position;

        position.x = (position.x - uiDocument.rootVisualElement.layout.width / 2);
        uiDocument.rootVisualElement.transform.position = position;
    }

    public void SetText(string text)
    {
        label.text = text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
