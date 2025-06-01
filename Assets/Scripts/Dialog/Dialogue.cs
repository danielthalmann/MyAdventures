using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Data", menuName = "Scriptable Objets/Dialogue", order = 1)]
public class Dialogue : ScriptableObject
{
    [Multiline(40)]
    public string dialogText;

    void OnGUI()
    {
        // Make a multiline text area that modifies stringToEdit.
        dialogText = GUILayout.TextArea(dialogText, 200);
    }

}
