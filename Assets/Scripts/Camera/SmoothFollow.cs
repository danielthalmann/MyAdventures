using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    protected Camera cam;

    protected Vector3 offset;

    public bool fixedPosition = false;
    public bool fixedRotation = false;
    public float smoothSpeed = 0.125f;
    public Transform target;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        offset = transform.position - target.position;
    }

    
    void Update()
    {
        if (!fixedPosition)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 SmoothPosition = Vector3.Lerp(cam.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
            cam.transform.position = SmoothPosition;
        }
        if (!fixedRotation)
        {
            Vector3 direction = target.transform.position - cam.transform.position;
            cam.transform.rotation = Quaternion.LookRotation(direction);
        }

    }



}
