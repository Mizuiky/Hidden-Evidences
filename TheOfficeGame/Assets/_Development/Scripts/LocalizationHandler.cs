using System;
using System.Collections.Generic;
using UnityEngine;
public enum Language
{
    PT,
    EN
}

[CreateAssetMenu(menuName = "Data/Dialog/LocalizationHandler")]
public class LocalizationHandler : ScriptableObject
{
    public TextAsset[] _localizationFiles;

    private Dictionary<string, string> PT_BR;
    private Dictionary<string, string> EN;
    private Dictionary<string, string>[] languages;

    private Language gameLanguage;
    private int languageIndex;

    public void Init()
    {
        PT_BR = new Dictionary<string, string>();
        EN = new Dictionary<string, string>();

        languages = new Dictionary<string, string>[2]
        {
            new Dictionary<string, string>(),
            new Dictionary<string, string>()
        };

        gameLanguage = Language.PT;

        for (int i = 0; i < _localizationFiles.Length; i++)
        {
            BuildDictionary(_localizationFiles[i], i);
        }
    }

    private void BuildDictionary(TextAsset file, int index)
    {
        Dictionary<string, string> readFiles = JsonHelper.Deserialize<Dictionary<string, string>>(file.ToString());

        foreach (KeyValuePair<string, string> entry in readFiles)
        {
            languages[index].Add(entry.Key, entry.Value);
        }

        //test
        GetTranslation("before_boss6");
    }

    public string GetTranslation(string key)
    {
        languageIndex = gameLanguage switch
        {
            Language.PT => languageIndex = 0,
            Language.EN => languageIndex = 1,
            _ => throw new ArgumentException(message: "invalid language")
        };

        string txt = languages[0][key];

        if (txt != "")
            return txt;

        return null;
    }

    public void ChangeCurrentLanguage(Language newLanguage)
    {
        gameLanguage = newLanguage;
    }
}
