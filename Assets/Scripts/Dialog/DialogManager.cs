using FMOD.Studio;
using FMODUnity;
using System;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Sentence[] sentences;
    private Sentence currentSentence;
    private int sentencesIndex;

    [Header("Dialog UI")]
    public bool dialogEnable;
    public GameObject dialogBox;
    public TMP_Text titleText;
    public TMP_Text dialogText;
    public Image dialogPicture;
    public Button[] dialogButtons;

    [Header("Dialog Character")]
    public string[] characters;
    public Sprite[] charactersSprites;

    [Header("Translate")]
    public DialogueTranslate translator;

    [Header("Event")]
    public UnityEvent onUnityDialogStart;
    public UnityEvent onUnityDialogEnd;


    public delegate void OnDialogStart(GameObject gameObject);
    public static OnDialogStart onDialogStart;
    
    public delegate void OnDialogEnd(GameObject gameObject);
    public static OnDialogEnd onDialogEnd;

    public delegate void OnDialogEventReference(string eventReference);
    public static OnDialogEventReference onDialogEventReference;

    public static DialogManager instance;

    private GameObject hitObject;


    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Found more than one Dialog Manager in the scene.");
        }

        instance = this;

    }

    // Start is called before the first frame update
    void Start()
    {
        dialogBox.SetActive(dialogEnable);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EndDialogue();
        }
    }

    public void StartObjectDialog(GameObject o)
    {
        hitObject = o;
        if (hitObject != null)
        {
            DialogTrigger trigger = hitObject.GetComponent<DialogTrigger>();
            if (trigger != null)
            {
                this.StartDialogue(trigger.dialogue);
                //hitObject = null;
            }
        }
    }

    public void PressOption(int index)
    {
        if (currentSentence.options.Length == 1)
        {
            if (currentSentence.options[0].jumpToTag != "")
            {
                for (int i = 0; i < sentences.Length; i++)
                {
                    if (currentSentence.options[0].jumpToTag != "" && sentences[i].tag == currentSentence.options[0].jumpToTag)
                    {
                        sentencesIndex = i;
                        break;
                    }
                }

            }
            if (currentSentence.options[0].eventReference != null)
            {
                onDialogEventReference?.Invoke(currentSentence.options[0].eventReference);
            }
            DisplayNextSentence();

        }
        else
        {
            if (index < currentSentence.options.Length)
            {

                for(int i = 0; i < sentences.Length; i++)
                {
                    if (currentSentence.options[index].jumpToTag != "" && sentences[i].tag == currentSentence.options[index].jumpToTag)
                    {
                        sentencesIndex = i;
                        break;
                    }
                }

                // currentSentence.options[index].
                Debug.Log(currentSentence.options[index].eventReference);
                if (currentSentence.options[index].eventReference != null)
                {
                    onDialogEventReference?.Invoke(currentSentence.options[index].eventReference);
                }
                DisplayNextSentence();

            }
        }

    }


    public Sentence[] CompileText(string text)
    {
        string[] lines = text.Split('\n');
        Sentence[] sentences = new Sentence[0];

        int index = 0;
        Sentence sentence;
        do
        {
            sentence = CreateSentence(lines, ref index, 0);
            if (sentence != null)
            {
                Array.Resize(ref sentences, sentences.Length + 1);
                sentences[sentences.Length - 1] = sentence;
            }

        } while (sentence != null);

        return sentences;

    }

    public Sentence CreateSentence(string[] lines, ref int index, int level)
    {
        string line;
        DialogOption option;

        line = forwardBlank(lines, ref index);

        if (line != null && line.StartsWith('#'))
        {
            Sentence sentence = new Sentence();
            sentence.tag = line.Substring(1).Trim();
            sentence.options = new DialogOption[0];

            option = new DialogOption();
            option.textOption = "Continuer";
            option.defaultOption = true;

            Array.Resize(ref sentence.options, sentence.options.Length + 1);
            sentence.options[sentence.options.Length - 1] = (option);


            index++;
            line = forwardBlank(lines, ref index);

            while (line != null && !line.StartsWith('#'))
            {

                if (line != null && line.StartsWith('@'))
                {
                    sentence.character = line.Substring(1).Trim();

                    index++;
                    line = forwardBlank(lines, ref index);

                } 
                else if (line != null && line.StartsWith('*'))
                { 
                    option = new DialogOption();
                    option.textOption = line.Substring(1).Trim();
                    option.defaultOption = false;

                    Array.Resize(ref sentence.options, sentence.options.Length + 1);
                    sentence.options[sentence.options.Length -1 ] = (option);

                    index++;
                    line = forwardBlank(lines, ref index);

                    if (line != null && line.StartsWith('>'))
                    {
                        option.jumpToTag = line.Substring(1).Trim();

                        index++;
                        line = forwardBlank(lines, ref index);
                    }

                    if (line != null && line.StartsWith('!'))
                    {
                        option.eventReference = line.Substring(1).Trim();

                        index++;
                        line = forwardBlank(lines, ref index);
                    }

                }
                else if (line != null && line.StartsWith('?'))
                {
                    sentence.soundReference = line.Substring(1).Trim();

                    index++;
                    line = forwardBlank(lines, ref index);

                }
                else if (line != null && line.StartsWith('>'))
                {
                    string jumpToTag = line.Substring(1).Trim();
                    if (jumpToTag == "")
                    {
                        sentence.end = true;
                    } else
                    {
                        sentence.options[0].jumpToTag = jumpToTag;
                    }

                    index++;
                    line = forwardBlank(lines, ref index);

                }else if (line != null && line.StartsWith('!'))
                {
                    sentence.options[0].eventReference = line.Substring(1).Trim();
                    Debug.Log(sentence.options[0].eventReference);

                    index++;
                    line = forwardBlank(lines, ref index);
                }
                else
                {
                    if (sentence.sentence != "")
                        sentence.sentence += '\n';
                    sentence.sentence += line;
                    index++;
                    line = forwardBlank(lines, ref index);
                }

            }

            return sentence;

        }

        return null;

    }

    private string forwardBlank(string[] lines, ref int index)
    {
        char[] sepTabBlank = { '\u0009', ' ' };

        string line;

        if (index < lines.Length)
            line = lines[index].Trim(sepTabBlank);
        else
            line = "";

        while (line == "" && index < lines.Length)
        {
            ++index;
            if (index < lines.Length)
                line = lines[index].Trim(sepTabBlank);
            else
                line = "";

        }
        
        if (index == lines.Length) {
            return null;
        }
        else { 
            return line;
        }
    }


    public void StartDialogue(Dialogue dialogue)
    {
        sentencesIndex = 0;
        sentences = CompileText(dialogue.dialogText);
       
        if (sentences.Length > 0)
        {
            clearUI();
            DisplayNextSentence();
            onDialogStart?.Invoke(hitObject);
        }

    }

    public void DisplayNextSentence ()
    {

        if (sentencesIndex >= sentences.Length)
        {
            EndDialogue();
            return;
        }

        if (currentSentence != null)
        {
            if (currentSentence.end)
            {
                EndDialogue();
                return;
            }

        }

        currentSentence = sentences[sentencesIndex];
        sentencesIndex++;
        FillUI();

        onUnityDialogStart.Invoke();
        PlaySpeek();

    }

    private void clearUI()
    {
        titleText.text = "";
        dialogText.text = "";
        dialogPicture.enabled = false;
        HideButtonUI();

    }

    private void DisplayPictureCharacter(string character)
    {
        dialogPicture.enabled = false;
        if (character != null)
        {


            for(int i = 0; i < characters.Length; i++)
            {
                if (characters[i].ToLower() == character.ToLower())
                {
                    if (i < charactersSprites.Length)
                    {
                        dialogPicture.sprite = charactersSprites[i];
                        dialogPicture.enabled = true;
                    }
                    break;
                }
            }

        }

    }

    private void HideButtonUI()
    {
        for (int i = 0; i < dialogButtons.Length; i++)
        {
            dialogButtons[i].gameObject.SetActive(false);
        }
    }

    private void FillUI()
    {
        TMP_Text tmp;

        // prepare dialog
        if (currentSentence.character != null)
        {
            if (currentSentence.character != "")
                titleText.text = currentSentence.character;
        }

        dialogText.text = Translate(currentSentence.sentence);
        DisplayPictureCharacter(titleText.text);
        HideButtonUI();

        if (currentSentence.options.Length == 1)
        {
            if (dialogButtons.Length > 0)
            {
                dialogButtons[0].gameObject.SetActive(true);
                tmp = dialogButtons[0].GetComponentInChildren<TMP_Text>();
                if(tmp != null) 
                    tmp.text = Translate(currentSentence.options[0].textOption);
            }
        } else
        {
            for (int i = 1; i < currentSentence.options.Length; i++)
            {
                if (i < dialogButtons.Length)
                {
                    dialogButtons[i].gameObject.SetActive(true);
                    tmp = dialogButtons[i].GetComponentInChildren<TMP_Text>();
                    if (tmp != null)
                        tmp.text = Translate(currentSentence.options[i].textOption);

                }
            }
        }


        dialogEnable = true;
        dialogBox.SetActive(dialogEnable);
    }


    public void EndDialogue()
    {
        StopSpeek();
        dialogEnable = false;
        currentSentence = null;
        dialogBox.SetActive(dialogEnable);
        onUnityDialogEnd.Invoke();
        onDialogEnd?.Invoke(hitObject);
     
    }

    private void PlaySpeek() 
    { 
    
        StopSpeek();
        // play sound

    }

    private void StopSpeek()
    {
        // stop sound
    }

    private string Translate(string text)
    {
        if (translator != null)
        {
            return translator.Translate(text);
        }
        return text;
    }

    public void Extract()
    {
        string dialogDirectory = Directory.GetCurrentDirectory() + "/dialog.txt";
        Dictionary<string, string> dictionary = new Dictionary<string, string>();

        string[] texts;

        foreach (Dialogue dialog in Resources.FindObjectsOfTypeAll(typeof(Dialogue)) as Dialogue[])
        {
            Debug.LogWarning(dialog.ToString());
            texts = GetTextsOfDialogue(dialog);
            foreach(String text in texts)
            {
                if(!dictionary.ContainsKey(text))
                {
                    dictionary.Add(text, text);
                }
            }
        }

        File.AppendAllLines(dialogDirectory, dictionary.Keys, System.Text.Encoding.UTF8);

    }

    public string[] GetTextsOfDialogue(Dialogue dialogue)
    {
        string[] texts = new string[0];

        Sentence[] compiledSentences = CompileText(dialogue.dialogText);
        foreach(Sentence sent in compiledSentences)
        {
            string text = sent.sentence.Replace("\r\n", "\n");
            Debug.Log(text);
            string[] lines = text.Split(new string[] { "\n", "\r" }, System.StringSplitOptions.None);

            if (lines.Length > 1)
            {
                for (int i = 0; i < lines.Length; ++i)
                {
                    Array.Resize(ref texts, texts.Length + 1);
                    texts[texts.Length - 1] = lines[i];
                    Debug.Log(lines[i]);
                }
            }
            else
            {
                Array.Resize(ref texts, texts.Length + 1);
                texts[texts.Length - 1] = text;
            }

            foreach (DialogOption option in sent.options)
            {
                Array.Resize(ref texts, texts.Length + 1);
                texts[texts.Length - 1] = option.textOption;
            }

        }

        return texts;
    }

    private void PlaySound(FMODUnity.EventReference eventReference)
    {
        /*
        StopSound();
        if (!eventReference.IsNull)
        {
            eventInstance = RuntimeManager.CreateInstance(eventReference);
            eventInstance.start();
        }
        */

    }

    private void StopSound()
    {
        /*
        if (eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
            eventInstance.release();
        }
        */
    }
}
