using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PointOfInterestManager : MonoBehaviour
{

    [Header("UI")]
    public PointOfInterestUI uiDocument;

    [Header("Events")]
    public UnityEvent onShow;
    public UnityEvent onHide;

    [Header("Translate")]
    public PointOfInterestTranslate translate;

    private PointOfInterestAbstract pointOfInterest;

    private static PointOfInterestManager instance;

    public static PointOfInterestManager getInstance()
    {
        if (instance == null)
        {
            instance = FindAnyObjectByType<PointOfInterestManager>();
        }

        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one PointOfInterestManager in the scene.");
        }

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        uiDocument.Hide();
    }

    public void SetPointOfInterest(PointOfInterestAbstract poi)
    {
        pointOfInterest = poi;
    }


    public void ShowPointOfInterest()
    {

        if (!uiDocument.IsShow())
        {
            uiDocument.SetText(Translate(pointOfInterest.title));
            uiDocument.Show();
            onShow.Invoke();
        }


        Vector3 vscreen = Camera.main.WorldToScreenPoint(pointOfInterest.transform.position + pointOfInterest.offset);
        Debug.Log(vscreen);
        uiDocument.SetPosition(vscreen);


    }

    private string Translate(string text)
    {
        if (translate)
        {
            return translate.Translate(text);
        }
        else
        {
            return text;
        }
    }

    public void HidePointOfInterest()
    {
        uiDocument.Hide();
        onHide.Invoke();
    }

}
