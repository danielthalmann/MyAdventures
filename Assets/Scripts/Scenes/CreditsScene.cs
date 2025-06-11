using UnityEngine;
using UnityEngine.SceneManagement;

public class CreditsScene : MonoBehaviour
{

    public Credits credits;

    private bool sceneLoaded = false;

    // Update is called once per frame
    void Update()
    {
        if (sceneLoaded)
            return;

        if (!credits.play)
        {
            sceneLoaded = true;
            SceneLoaderManager.instance.LoadScene("Menu");
        }

        if (Input.GetMouseButtonUp(0))
        {
            sceneLoaded = true;
            SceneLoaderManager.instance.LoadScene("Menu");
        }
        
    }
}
