using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static SmoothFollow;


public class SmoothFollow : MonoBehaviour
{

    [System.Serializable]
    public struct LockPosition
    {
        public bool x;
        public bool y;
        public bool z;
    };

    protected Camera cam;

    protected Vector3 offset;

    public bool fixedPosition = false;
    public bool fixedRotation = false;
    public float smoothSpeed = 0.125f;
    public Transform target;

    [SerializeField]
    public LockPosition lockPosition;

    Vector3 startPosition;

    void Start()
    {
        cam = this.GetComponent<Camera>();
        offset = transform.position - target.position;
        startPosition = transform.position;
    }

    
    void Update()
    {
        if (!fixedPosition)
        {
            Vector3 desiredPosition = target.position + offset;
            Vector3 SmoothPosition = Vector3.Lerp(cam.transform.position, desiredPosition, smoothSpeed * Time.deltaTime);

            if (lockPosition.x)
                SmoothPosition.x = startPosition.x;
            if (lockPosition.y)
                SmoothPosition.y = startPosition.y;
            if (lockPosition.z)
                SmoothPosition.z = startPosition.z;

            cam.transform.position = SmoothPosition;
        }
        if (!fixedRotation)
        {
            Vector3 direction = target.transform.position - cam.transform.position;
            cam.transform.rotation = Quaternion.LookRotation(direction);
        }

    }



}
