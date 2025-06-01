using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class StartTrigger : MonoBehaviour
{
    public UnityEvent onStart;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(InvokeEvent());
    }

    IEnumerator InvokeEvent()
    {
        yield return null;
        onStart.Invoke();
    }
}
