using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Translator : MonoBehaviour
{

    void Start()
    {
        TMP_Text text = GetComponent<TMP_Text>();

        if (text != null)
        {
            text.text = I18n.translate(text.text);
        }
    }

}
