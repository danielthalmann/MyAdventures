using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class Sentence
{
    /// <summary>
    /// tag pour reconnaitre l'étape
    /// </summary>
    public string tag;


    /// <summary>
    /// Nom de la personne qui parle
    /// </summary>
    public string character;

    /// <summary>
    /// Phrase
    /// </summary>
    [TextArea]
    public string sentence;
    
    /// <summary>
    /// identifiant du son
    /// </summary>
    public string soundReference;

    /// <summary>
    /// indique que c'est le dernier message
    /// </summary>
    public bool end;

    /// <summary>
    /// Option du dialogue
    /// </summary>
    public DialogOption[] options;
	

}
