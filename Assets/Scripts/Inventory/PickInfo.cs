using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickInfo : MonoBehaviour
{
    Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        camera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 v = transform.position;

        v -= camera.transform.position;
        v.y = 0;
        v = v.normalized;

        transform.rotation = Quaternion.LookRotation(v);

    }
}
