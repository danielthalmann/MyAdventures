using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionManager : MonoBehaviour
{

    public static ActionManager instance;

    private GameObject hitObject;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Action Manager in the scene.");
        }

        instance = this;

    }

    public void OnHitObject(GameObject o)
    {
        hitObject = o;
    }

    public void ReachHitObject() 
    {
        if (hitObject != null)
        {
            ActionTrigger trigger = hitObject.GetComponent<ActionTrigger>();

            if (trigger != null)
            {
                trigger.Trigger();
            }
        }
    }


}
