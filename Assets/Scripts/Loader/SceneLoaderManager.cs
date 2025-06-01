using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoaderManager : MonoBehaviour
{

    public Image fadeImage;
    public Color fadeColor;
    public float duration;

    private static SceneLoaderManager instance;

    string currentSceneName = null;

    public Action onStartLoading;
    public Action onFinishLoading;


    public static SceneLoaderManager getInstance()
    {
        if (instance == null)
        {
            instance = FindAnyObjectByType<SceneLoaderManager>();
        }

        return instance;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Scene Loader Manager in the scene.");
        }

        instance = this;

    }

    public void loadScene(string sceneName)
    {
        currentSceneName = sceneName;
        onStartLoading?.Invoke();
        StartCoroutine(FadeLoadingScreen(2, true));
    }

    // Start is called before the first frame update
    void Start()
    {
        onStartLoading?.Invoke();
        StartCoroutine(FadeLoadingScreen(2));
    }

    IEnumerator FadeLoadingScreen(float duration, bool fadeOut = false)
    {
        Color currentColor = fadeColor;
        float startValue = currentColor.a;
        float time = 0;
        fadeImage.gameObject.SetActive(true);

        while (time < duration)
        {
            if (fadeOut)
                currentColor.a = Mathf.Lerp(startValue, 1, time / duration);
            else
                currentColor.a = 1f - Mathf.Lerp(startValue, 1, time / duration);

            time += Time.deltaTime;
            fadeImage.color = currentColor;

            yield return null;
        }
        
        if (fadeOut)
        {
            onFinishLoading?.Invoke();
            AsyncOperation async = SceneManager.LoadSceneAsync(currentSceneName);
        } else
        {
            onFinishLoading?.Invoke();
            fadeImage.gameObject.SetActive(false);
        }
    }

}
