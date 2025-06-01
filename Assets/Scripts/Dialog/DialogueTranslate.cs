using UnityEditor;
using UnityEngine;


public class DialogueTranslate : MonoBehaviour, TranslateInterface
{
    public string Translate(string text)
    {
        return I18n.Trans().TranslateMulti(text);
    }
}
