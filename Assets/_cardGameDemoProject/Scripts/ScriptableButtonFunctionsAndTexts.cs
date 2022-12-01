using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Button strings and function data")]

public class ScriptableButtonFunctionsAndTexts : ScriptableObject, IButtonFunctions
{
    [Header("Button")]
    public ScriptableLanguageSelector _languageData;

    public Sprite _buttonImage;
    public string _buttonString
    {
        get
        {
            return _languageData._choosenLanguage._backButton;
        }
    }

    /*[Space(10)]
    [Header("Texts")]*/

    public string _notificationQuestion
    {
        get
        {
            return _languageData._choosenLanguage._notificationQuestion;
        }
    }
    public string _notification
    {
        get
        {
            return _languageData._choosenLanguage._notification;
        }
    }
}