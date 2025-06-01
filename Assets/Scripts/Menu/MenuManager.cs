using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public string startSceneName;
    public string CreditsSceneName;
    public string menuSceneName;

    public void ButtonMenu()
    {
        SceneLoaderManager.getInstance().loadScene(menuSceneName);
    }


    public void ButtonStart()
    {
        SceneLoaderManager.getInstance().loadScene(startSceneName);
    }

    public void ButtonOptions()
    {

    }

    public void ButtonCredits()
    {
        SceneLoaderManager.getInstance().loadScene(CreditsSceneName);
    }

    public void SetLanguage(string language)
    {
        I18n.Trans().SetLanguage(language);
    }


    public void ButtonQuit()
    {
        Debug.Log("Quit");
        Application.Quit();
    }

}
