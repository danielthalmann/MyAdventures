using UnityEditor;
using UnityEngine;


public class PointOfInterestTranslate : MonoBehaviour, TranslateInterface
{
    public string Translate(string text)
    {
        return I18n.Trans().TranslateMulti(text);
    }
}
