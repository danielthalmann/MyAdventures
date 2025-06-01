using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointOfInterest2D : PointOfInterestAbstract
{

    private Collider2D PoIcollider2D;
    private bool overlap;

    // Start is called before the first frame update
    void Start()
    {
        PoIcollider2D = this.GetComponent<Collider2D>();
        if (PoIcollider2D == null)
        {
            Debug.LogError("PointOfInterest2D need one collider2D to work correctly.");
        }

    }

    private void Update()
    {

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 touchPos = new Vector2(mousePosition.x, mousePosition.y);

        if (this.PoIcollider2D.OverlapPoint(touchPos))
        {
            Debug.Log("OverlapPoint");
            Debug.Log(mousePosition);
            if (!overlap)
            {
                overlap = true;
                PointOfInterestManager.getInstance().SetPointOfInterest(this);
                PointOfInterestManager.getInstance().ShowPointOfInterest();
            } 

        } else
        {
            if (overlap)
            {
                overlap = false;
                PointOfInterestManager.getInstance().HidePointOfInterest();
            }
        }
    }

}
