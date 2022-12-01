using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class BackToGameButtonScript : MonoBehaviour
{
    PublicValuesAndFunctions _publicValues;
    Button _this;
    private void Awake()
    {
        _this = GetComponent<Button>();
        if (!_publicValues)
        {
            _publicValues = GameObject.FindWithTag("PublicValues").GetComponentInChildren<PublicValuesAndFunctions>();
        }
        _this.onClick.AddListener(ButtonFunction);
    }
    private void Start()
    {

    }
    public void ButtonFunction()
    {
        _publicValues.ActivateNotificationPanel(false);
    }
}