using UnityEngine;

using UnityEngine.UIElements;


[RequireComponent(typeof(UIDocument))]
public class PauseMenu : MonoBehaviour
{
    public string sceneQuit = "Menu";

    UIDocument document;

    public delegate void OnPause();
    public OnPause onPause;

    public delegate void OnResume();
    public OnResume onResume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }

    private void OnEnable()
    {
        document = GetComponent<UIDocument>();
        document.rootVisualElement.Q<Button>("ButtonResume").clicked += Resume;
        document.rootVisualElement.Q<Button>("ButtonQuit").clicked += Quit;
        document.rootVisualElement.Q<Button>("ButtonSave").clicked += Save;
        document.rootVisualElement.Q<Button>("ButtonRestore").clicked += Restore;
    }

    private void OnDisable()
    {
        //document.rootVisualElement.Q<Button>("ButtonResume").clicked -= Resume;
        //document.rootVisualElement.Q<Button>("ButtonQuit").clicked -= Quit;
        //document.rootVisualElement.Q<Button>("ButtonSave").clicked -= Save;
        //document.rootVisualElement.Q<Button>("ButtonRestore").clicked -= Restore;
    }

    public void Pause()
    {
        gameObject.SetActive(true);
        onPause?.Invoke();
    }

    public void Resume()
    {
        gameObject.SetActive(false);
        onResume?.Invoke();
    }

    private void Quit()
    {
        SceneLoaderManager.instance.LoadScene(sceneQuit);
    }

    private void Save()
    {
        SaveSystem.Save();
        Resume();
    }

    private void Restore()
    {
        SaveSystem.Load();
        Resume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
