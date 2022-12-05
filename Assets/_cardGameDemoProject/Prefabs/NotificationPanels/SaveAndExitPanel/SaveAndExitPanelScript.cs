using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveAndExitPanelScript : MonoBehaviour
{
    [HideInInspector] public PublicValuesAndFunctions _publicValues;
    [HideInInspector] public Button _this;
    private void Start()
    {

    }
    private void OnEnable()
    {
        if (GetComponent<Button>())
        {
            _this = GetComponent<Button>();
        }
        if (!_publicValues)
        {
            _publicValues = GameObject.FindWithTag("PublicValues").GetComponentInChildren<PublicValuesAndFunctions>();
        }
        _this.onClick.AddListener(ButtonFunction);
    }
    public void ButtonFunction()
    {
        _publicValues.SaveAndExit();
    }
}