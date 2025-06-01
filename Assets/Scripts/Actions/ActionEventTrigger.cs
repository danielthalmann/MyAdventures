using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class ActionEventTrigger : ActionTrigger

{
    public UnityEvent onTrigged;

    public override void Trigger()
    {
        onTrigged.Invoke();
    }

}
