using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public bool playerEnable = false;

    public delegate void OnChangePlayerEnabled(bool enabled);

    public static OnChangePlayerEnabled onChangePlayerEnabled;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Game Manager in the scene.");
        }

        instance = this;
    }


    public void setPlayerEnabled(bool enabled)
    {
        playerEnable = enabled;
        onChangePlayerEnabled?.Invoke(enabled);
    }



}
