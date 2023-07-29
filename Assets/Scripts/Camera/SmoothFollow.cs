using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{

    protected Camera cam;

    protected Vector3 offset;
    public float smoothSpeed = 0.125f;
    public Transform target;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        offset = transform.position - target.position;
    }

    
    void Update()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 SmoothPosition = Vector3.Lerp(cam.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);
        cam.transform.position = SmoothPosition;
    }


}
