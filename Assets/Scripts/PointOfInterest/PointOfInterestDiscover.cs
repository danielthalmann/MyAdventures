using System.Collections.Generic;
using UnityEngine;

public class PointOfInterestDiscover : MonoBehaviour
{
    public float recurrence;
    public GameObject star;

    private bool pause;
    private float timer;

    private PointOfInterest[] interests;

    private void Awake()
    {
        DialogManager.onDialogStart += OnDialogStart;
        DialogManager.onDialogEnd += OnDialogEnd;
    }


    void OnDialogStart(GameObject gamoeObject)
    {
        pause = true;
    }

    void OnDialogEnd(GameObject gamoeObject)
    {
        pause = false;
        timer = 0;
    }


    // Start is called before the first frame update
    void Start()
    {
        interests = FindObjectsByType<PointOfInterest>(FindObjectsSortMode.InstanceID);
        timer = 0;
        pause = false;
    }

    // Update 
    void FixedUpdate()
    {
        if (recurrence > 0)
        {
            if (!pause)
                timer += Time.deltaTime;

            if (timer > recurrence)
            {
                ShowPointOfInterest();
                timer = 0;
            }

        }

    }

    void ShowPointOfInterest()
    {

        List<PointOfInterest> list = new List<PointOfInterest>();

        foreach(PointOfInterest interest in interests)
        {
            if (interest.isActiveAndEnabled)
            {
                list.Add(interest);
            }
        }

        if (list.Count > 0)
        {
            int pos = Random.Range(0, list.Count);

            // GameObject newStar = GameObject.Instantiate(star);
            // FixDistanceToCamera fdc = newStar.GetComponent<FixDistanceToCamera>();
            // fdc.target = list[pos].gameObject.transform;
            // fdc.offset = list[pos].offset;
            // Destroy(newStar, 3);
        }

    }
}
