using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Card Game Objects/Language Selector/Language Selector", fileName = "Language Selector")]
public class ScriptableLanguageSelector : ScriptableObject
{
    public enum LanguageSelector
    {
        English,
        Turkish
    }
    [Header("Choose Langugage")]
    public LanguageSelector _languageSelector;

    [HideInInspector] public ScriptableStringFileAccordingToLanguage _choosenLanguage
    {
        get
        {
            if (_languageSelector == LanguageSelector.English)
            {
                if (_englishStrings != null)
                {
                    return _englishStrings;
                }
                else
                {
                    return null;
                }
            }
            else if (_languageSelector == LanguageSelector.Turkish)
            {
                if (_TurkishStrings != null)
                {
                    return _TurkishStrings;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                return null;
            }
        }
    }

    [Space(10)]
    [Header("Language Pack Objects")]
    public ScriptableStringFileAccordingToLanguage _englishStrings;
    public ScriptableStringFileAccordingToLanguage _TurkishStrings;
}