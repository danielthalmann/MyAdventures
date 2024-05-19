using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using Mono.Cecil;
using UnityEngine.Rendering;

public class DialogManager : MonoBehaviour
{
    public Queue<Sentence> sentences = new Queue<Sentence>();
    public TMP_Text nameText;
    public TMP_Text dialogText;
    public GameObject dialogBox;
    public bool dialogEnable;

    private EventInstance eventInstance;


    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(dialogEnable);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartDialogue(Dialogue dialogue)
    {

        sentences.Clear();

        GameManager.instance.setPlayerEnabled(false);

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        dialogEnable = true;
        dialogBox.SetActive(dialogEnable);
        DisplayNextSentence();

    }

    public void DisplayNextSentence ()
    {

        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        if(sentence.character != null)
        {
            if (sentence.character != "")
                nameText.text = sentence.character;
        }
        dialogText.text = sentence.sentence;

        PlaySound(sentence.soundReference);

    }

    public void EndDialogue()
    {
        StopSound();
        dialogEnable = false;
        dialogBox.SetActive(dialogEnable);
        GameManager.instance.setPlayerEnabled(true);

    }

    private void PlaySound(FMODUnity.EventReference eventReference)
    {
        StopSound();
        if (!eventReference.IsNull)
        {
            eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.start();
        }

    }

    private void StopSound()
    {
        if (eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
    }
}
