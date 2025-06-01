using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    public bool isTrigger = false;

    void Start()
    {
     
    }

    public void TriggerDialogue()
    {
        Debug.Log("TriggerDialogue");
        FindFirstObjectByType<DialogManager>().StartObjectDialog(gameObject);
    }

    public void OnTriggerEnter(Collider other)
    {
        if(isTrigger)
            TriggerDialogue();
    }
}
