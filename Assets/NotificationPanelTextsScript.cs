using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NotificationPanelTextsScript : MonoBehaviour
{
    [Header("Data")]
    public ScriptableButtonFunctionsAndTexts _buttonAndTextData;

    [Space(10)]
    [Header("Hierarchy")]
    public TextMeshProUGUI _question;
    public TextMeshProUGUI _notifications;
    private void Awake()
    {
        _question.text = _buttonAndTextData._notificationQuestion;
        _notifications.text = _buttonAndTextData._notification;
    }
}