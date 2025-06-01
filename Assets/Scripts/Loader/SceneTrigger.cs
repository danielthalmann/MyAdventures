using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneTrigger : ActionTrigger
{

    public string sceneName;


    public override void Trigger()
    {
        SceneLoaderManager.getInstance().loadScene(sceneName);
    }

}
