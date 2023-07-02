using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Globalization;


public class I18n
{
    public static Dictionary<string, string> dictionary;
    public static string fallback_lang = "en";

    static I18n()
    {
        dictionary = new Dictionary<string, string>();

        SystemLanguage currentLanguage = Application.systemLanguage;

        string lang = "en";

        foreach (CultureInfo culture in CultureInfo.GetCultures(CultureTypes.AllCultures))
        {
            if(culture.EnglishName.Equals(currentLanguage.ToString()))
            {
                lang = culture.Name;
                break;
            }
        }

        loadTranslate(lang);

    }

    public static string translate(string text)
    {
        if (dictionary.ContainsKey(text))
        {
            return dictionary[text];
        }
        else
        {
            return text;
        }
    }

    public static void loadTranslate(string lang)
    {
        dictionary.Clear();

        TextAsset textAsset = Resources.Load<TextAsset>("I18n/" + lang);

        if (textAsset == null)
        { 
            textAsset = Resources.Load<TextAsset>("I18n/" + fallback_lang); 
        }

        if (textAsset == null)
        {
            Debug.LogError("File not found for I18n: Assets/Resources/I18n/" + lang + ".txt");
            return;
        }

        string allTexts = "";
        allTexts = textAsset.text;

        string[] lines = allTexts.Split(new string[] { "\r\n", "\n" }, System.StringSplitOptions.None);
        
        string key, value;
        for (int i = 0; i < lines.Length; i++)
        {
            int pos = lines[i].IndexOf("=");
            if (pos >= 0 && !lines[i].StartsWith("#"))
            {
                key = lines[i].Substring(0, pos).Trim();
                value = lines[i].Substring(pos + 1, lines[i].Length - pos - 1).Replace("\\n", System.Environment.NewLine).Trim();

                if(!dictionary.ContainsKey(key))
                {
                    dictionary.Add(key, value);
                }

            }
        }
    }
}

