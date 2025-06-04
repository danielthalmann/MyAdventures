using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    void Start()
    {
     
    }

    public void TriggerDialogue()
    {
        DialogManager.instance.StartObjectDialog(gameObject);
    }

}
