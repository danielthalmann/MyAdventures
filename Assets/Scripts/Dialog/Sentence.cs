using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

[System.Serializable]
public class Sentence
{
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
    public EventReference soundReference;

}
