using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActionOnRun : MonoBehaviour
{

    public UnityEvent onStart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitAndStart());
    }

    IEnumerator WaitAndStart()
    {
        yield return new WaitForSeconds(0.001f);
        onStart.Invoke();
        yield return null;
    }

}
