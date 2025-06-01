using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PointOfInterest : PointOfInterestAbstract
{

    private bool mouseOnInterest = false;

    // Start is called before the first frame update
    void Start()
    {

        if (this.GetComponent<Collider>() == null)
        {
            Debug.LogError("PointOfInterest need one collider to work correctly.");
        }

    }

    public void FixedUpdate()
    {

        if (mouseOnInterest)
        {
            PointOfInterestManager.getInstance().SetPointOfInterest(this);
            PointOfInterestManager.getInstance().ShowPointOfInterest();
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position + offset, .05f);

        if (activeDestination)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawSphere(GetPointOfInterestDestination(), .05f);
        }

    }

    private void OnMouseEnter()
    {

        mouseOnInterest = true;

    }

    public Vector3 GetPointOfInterestDestination()
    {
        if (activeDestination)
            return transform.position + destination;
        else
            return transform.position;
    }

    private void OnMouseExit()
    {
        mouseOnInterest = false;
        PointOfInterestManager.getInstance().SetPointOfInterest(null);
        PointOfInterestManager.getInstance().HidePointOfInterest();
    }

}
