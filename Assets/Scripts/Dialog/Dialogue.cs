using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objets/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    public Sentence[] sentences;
}
