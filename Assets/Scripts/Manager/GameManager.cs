using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance { get; private set; }

    public bool lockPlayerMovement = false;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than on Game Manager in the scene.");
        }

        instance = this;
    }


    // Start is called before the first frame update
    void Start()
    {

    }

}
