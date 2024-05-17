using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Rendering;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public Queue<string> sentences = new Queue<string>();
    public TMP_Text nameText;
    public TMP_Text dialogText;
    public GameObject dialogBox;
    public bool dialogEnable;


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
        nameText.text = dialogue.name;
        foreach (string sentence in dialogue.sentences)
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

        string sentence = sentences.Dequeue();
        dialogText.text = sentence;

    }

    public void EndDialogue()
    {
        dialogEnable = false;
        dialogBox.SetActive(dialogEnable);
    }
}
