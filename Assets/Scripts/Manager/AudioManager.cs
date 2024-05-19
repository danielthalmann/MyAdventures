using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;

public class AudioManager : MonoBehaviour
{

    
    private EventInstance musicEventInstance;
    private List<EventInstance> eventInstances;
    


    [field: Header("Music")]

    [field: SerializeField]
    public EventReference music { get; set; }

    [field: Header("Player")]

    [field: SerializeField]
    public EventReference footSteps { get; set; }


    public static AudioManager instance { get; private set; }

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Found more than on Audio Manager in the scene.");
        }
        
        instance = this;
        eventInstances = new List<EventInstance>();
    }

    // Start is called before the first frame update
    void Start()
    {

        InitializeMusic(music);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// Démarre la musique du jeu
    /// </summary>
    /// <param name="musicReference"></param>
    public void InitializeMusic(EventReference musicReference)
    {
        musicEventInstance = CreateInstance(musicReference);
        musicEventInstance.start();

        musicEventInstance.setParameterByName("intensity", 1);

    }


    /// <summary>
    /// Ajoute une nouvelle instance du son
    /// </summary>
    /// <param name="eventReference"></param>
    /// <returns></returns>
    public EventInstance CreateInstance(EventReference eventReference)
    {
        EventInstance instance =  RuntimeManager.CreateInstance(eventReference);
        eventInstances.Add(instance);
        return instance;
    }


    public void PlayOneShot(EventReference sound, Vector3 worldPos)
    {
        RuntimeManager.PlayOneShot(sound, worldPos);
    }

    public void PlayOneShot(EventReference sound)
    {
        RuntimeManager.PlayOneShot(sound, Vector3.zero);
    }

    private void CleanUp()
    {
        foreach(EventInstance eventInstance in eventInstances)
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }

    private void OnDestroy()
    {
        CleanUp();
    }

}
