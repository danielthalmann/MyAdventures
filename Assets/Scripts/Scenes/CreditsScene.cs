using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{

    public Credits credits;
    public SceneLoaderManager sceneManager;

    private bool sceneLoaded = false;

    // Update is called once per frame
    void Update()
    {
        if (sceneLoaded)
            return;

        if (!credits.play)
        {
            sceneLoaded = true;
            sceneManager.loadScene("Menu");
        }

        if (Input.GetMouseButtonUp(0))
        {
            sceneLoaded = true;
            sceneManager.loadScene("Menu");
        }
        
    }
}
