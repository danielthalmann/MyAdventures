using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraOrigin : MonoBehaviour
{

    public bool moveCamera = false;
    public float smoothSpeed = 12f;
    public Camera cam;
    public Transform targetPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (moveCamera)
        {
            Vector3 SmoothPosition = Vector3.Lerp(cam.transform.position, targetPosition.position, smoothSpeed / 10 * Time.deltaTime);
            cam.transform.position = SmoothPosition;

            Vector3 dif = cam.transform.position - targetPosition.position;
            if (dif.magnitude < .1f)
            {
                moveCamera = false;
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
        if (other.tag == "Player")
        {
            moveCamera = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        moveCamera = false;
    }
}
