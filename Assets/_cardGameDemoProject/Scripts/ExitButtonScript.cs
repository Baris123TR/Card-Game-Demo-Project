using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExitButtonScript : MonoBehaviour
{
    [Header("Langugage Data")]
    public ScriptableLanguageSelector _languageAndStringData;

    PublicValuesAndFunctions _publicValues;

    [Space(10)]
    [Header("Hierarcyh")]
    TextMeshProUGUI _buttonText;
    Button _exitButton;
    private void Awake()
    {
        if (!_publicValues)
        {
            _publicValues = GameObject.FindWithTag("PublicValues").GetComponentInChildren<PublicValuesAndFunctions>();
        }
        if (GetComponentInChildren<TextMeshProUGUI>())
        {
            _buttonText = GetComponentInChildren<TextMeshProUGUI>();
        }
        _buttonText.text = _languageAndStringData._choosenLanguage._exitButton;
        if (GetComponent<Button>())
        {
            _exitButton = GetComponent<Button>();
        }
        _exitButton.onClick.AddListener(ButtonFunction);
    }
    private void Start()
    {
        
    }
    public void ButtonFunction()
    {
        _publicValues.ExitButton();
    }
}